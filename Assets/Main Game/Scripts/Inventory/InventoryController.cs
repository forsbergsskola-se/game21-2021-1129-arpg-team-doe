using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] ItemDatabaseObject itemsInDatabase;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    [SerializeField] GameObject rightClickMenuHolder;
    [SerializeField] GameObject rightClickMenu;
    [SerializeField] GameObject rightClickMenuSlots;
    [SerializeField] GameObject spawnedObject;
    
    
    [SerializeField] GameObject itemDisplayInfo;
    [SerializeField] int maxDisplayWidth = 200;
    //Item Description
    [SerializeField] GameObject itemDisplayTextBackground;
    [SerializeField] GameObject itemDisplayInfoText;
    
    TextMeshProUGUI itemDisplayText;
    //Item Name
    [SerializeField] GameObject itemDisplayNameBackground;
    [SerializeField] GameObject itemDisplayInfoName;
    
    TextMeshProUGUI itemDisplayName;
    
    [HideInInspector]
    public ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid{
        get => selectedItemGrid;
        set{
            selectedItemGrid = value;
            _inventoryHighlight.SetParent(value);
        }
    }

    public GameObject canvasInventory;
    [HideInInspector]public InventoryItem selectedItem;
    public Vector2Int pickUpPosition;
    public FMODUnity.EventReference inventoryReference;
    public InventoryItem lastRightClickedItem;
    GameObject DroppedObject{ get; set; }
    InventoryItem _overlapItem;
    InventoryItem _hoveredItem;
    RectTransform _rectTransform;
    InventoryHighlight _inventoryHighlight;
    InventoryItem _itemToHighlight;
    Transform _playerTransform;
    Consumer _playerConsumer;
    UIStats[] _uiStatsArray;
    Vector2Int _oldPosition;
    Vector2Int _pickOldPosition;
    EventInstance _inventoryInstance;
    bool _clickOnInventory;
    Vector3 _itemDisplayTextBackgroundOffset;

    void Awake(){
        _inventoryHighlight = GetComponent<InventoryHighlight>();
        
    }

    void Start(){
        itemDisplayText = itemDisplayInfoText.GetComponent<TextMeshProUGUI>();
        itemDisplayName = itemDisplayInfoName.GetComponent<TextMeshProUGUI>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerConsumer = _playerTransform.GetComponent<Consumer>();
         _uiStatsArray = FindObjectsOfType<UIStats>();
         _inventoryInstance = FMODUnity.RuntimeManager.CreateInstance(inventoryReference);
    }

    void Update(){
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.I) && !SkillPointApplyCheck()){
            ToggleInventory();
            PlayInventorySound();
        }

        if (Input.GetKeyDown(KeyCode.R)){
            RotateItem(); 
        }

        if (selectedItemGrid == null){
            _inventoryHighlight.Show(false);
            return;
        }
        
        HandleHighlight();
        if (Input.GetMouseButtonDown(0)){
            LeftMouseButtonPress();
        }
        if (Input.GetMouseButtonUp(0)){
            LeftMouseButtonRelease();
        }
        
        MouseOver(); //if mouse over, display item object stats etc. _selecteditem.itemobject.whatever

        if (Input.GetMouseButtonDown(1)){
            RightMouseButtonPress();
        }
    }

    public void InsertItem(InventoryItem itemToInsert){
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);
        if (posOnGrid == null){
            return;
        }
        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }
    
    public InventoryItem CreateItem(int selectedItemID){
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvasTransform);
        _rectTransform.SetAsLastSibling();
        inventoryItem.Set(itemsInDatabase.GetItem[selectedItemID]);
        return inventoryItem;
    }
    
    public void UseItemButton() //Called by button
    {
        if (lastRightClickedItem.itemObject is ConsumableObject)
        {
            _playerConsumer._consumableObject = (ConsumableObject) lastRightClickedItem.itemObject;
            _playerConsumer.Consume();
            _playerConsumer._consumableObject = null;
            RemoveItemFromInventoryRight();
        }
        else
        {
            lastRightClickedItem.itemObject.UseItem();
        }
    }
    public void UseItem() //Called by button
    {
        if (selectedItem.itemObject is ConsumableObject)
        {
            _playerConsumer._consumableObject = (ConsumableObject) selectedItem.itemObject;
            _playerConsumer.Consume();
            _playerConsumer._consumableObject = null;
            RemoveItemFromInventory();
        }
        else
        {
            selectedItem.itemObject.UseItem();
        }
    }
   
    public void PlaceItem(Vector2Int tileGridPosition){
        if (selectedItemGrid.IsOutOfInventoryGrid(tileGridPosition.x, tileGridPosition.y)){
            DropItemToGround();
            return;
        }
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref _overlapItem);
        if (complete){
            selectedItem = null;
            if (_overlapItem != null){
                selectedItem = _overlapItem;
                _overlapItem = null;
                _rectTransform = selectedItem.GetComponent<RectTransform>();
                _rectTransform.SetAsLastSibling(); //item you hold dont get behind a placed item in inventory
            }
        }
    }
    
    void MouseOver(){
        
        var tileGridPosition = GetTileGridPosition();
        if (!selectedItemGrid.IsOutOfInventoryGrid(tileGridPosition.x, tileGridPosition.y))
        {
            _hoveredItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);
            DisplayItemInformation();
        }
        else if (itemDisplayTextBackground.activeInHierarchy)
        {
            DeactivateItemInformationDisplay();
        }  
        
        
    }

    void DisplayItemInformation()
    {
        if (_hoveredItem != null && _hoveredItem.itemObject.description != null)
        {
            if (!itemDisplayInfo.activeInHierarchy)
            {
                itemDisplayInfo.SetActive(true);
            }

            DisplayItemName();

            DisplayItemDescription();
        }
        else if (_hoveredItem == null)
        {
            DeactivateItemInformationDisplay();
        }
    }

    void DisplayItemDescription()
    {
        itemDisplayText.text = _hoveredItem.itemObject.description;
        itemDisplayTextBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(itemDisplayText.preferredWidth, 0, maxDisplayWidth) + 10,
            (Mathf.Clamp(itemDisplayText.preferredHeight, 10, maxDisplayWidth)) + 10);
    }

    void DisplayItemName()
    {
        if (_hoveredItem.itemObject is ConsumableObject)
        {
            itemDisplayName.color = Color.yellow;
        }
        else {
            itemDisplayName.color = Color.white;
        }
        itemDisplayName.text = _hoveredItem.itemObject.name;
        itemDisplayNameBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(
            Mathf.Clamp(itemDisplayName.preferredWidth, 0, maxDisplayWidth) + 10,
            (Mathf.Clamp(itemDisplayName.preferredHeight, 10, maxDisplayWidth)) + 10);
    }

    void DeactivateItemInformationDisplay()
    {
        itemDisplayInfo.SetActive(false);
        itemDisplayText.text = "No Description Available";
        itemDisplayName.text = "No Name Available";
    }

    void ToggleInventory(){
        canvasInventory.SetActive(!canvasInventory.activeInHierarchy);
    }

    void DropItemToGround(){
        SpawnItemOnGround();
        RemoveItemFromInventory();
    }

    void RotateItem(){
        if (selectedItem == null){
            return;
        }
        selectedItem.Rotate();
    }

    void HandleHighlight(){
        Vector2Int positionOnGrid = GetHighlightTileGridPosition();
        if (_oldPosition == positionOnGrid){
            return;
        }
        _oldPosition = positionOnGrid;

        if (selectedItem == null){
            _itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (_itemToHighlight != null){
                _inventoryHighlight.Show(true);
                _inventoryHighlight.SetSize(_itemToHighlight);
                _inventoryHighlight.SetPosition(selectedItemGrid, _itemToHighlight);
            }
            else{
                _inventoryHighlight.Show(false);
            }
        }
        else{
            
            _inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(positionOnGrid.x, positionOnGrid.y, 
                selectedItem.WIDTH, selectedItem.HEIGHT));
            _inventoryHighlight.SetSize(selectedItem);
            _inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, 
                positionOnGrid.y);
        }
    }

    void RemoveItemFromInventory(){
        Destroy(selectedItem.gameObject);
        selectedItem = null;
    }
    void RemoveItemFromInventoryRight(){
        Destroy(lastRightClickedItem.gameObject);
        lastRightClickedItem = null;
    }
    
    void SpawnItemOnGround(){
        var droppedItem = spawnedObject;
        droppedItem.GetComponent<InventoryItem>().itemObject = selectedItem.itemObject;
        Vector3 spawnPosition = _playerTransform.position + new Vector3(0, 0, 2);
        DroppedObject = Instantiate(droppedItem, spawnPosition, Quaternion.identity);
        DroppedObject.name = droppedItem.name;
        selectedItem.itemObject.PlayDropSound();
    }

    void LeftMouseButtonPress(){
        var tileGridPosition = GetTileGridPosition();
        _pickOldPosition = tileGridPosition;
        if (selectedItem == null){
            PickUpItem(tileGridPosition);
            if (selectedItem != null){
                pickUpPosition.x = selectedItem.onGridPositionX;
                pickUpPosition.y = selectedItem.onGridPositionY;
            }
        }
    }
    
    void LeftMouseButtonRelease(){
        var tileGridPosition = GetTileGridPosition();
        var highlightTileGridPosition = GetHighlightTileGridPosition();
        if (selectedItem != null){
            PlaceItem(tileGridPosition == _pickOldPosition ? pickUpPosition : highlightTileGridPosition);
        }
    }

    void RightMouseButtonPress(){
        var tileGridPosition = GetTileGridPosition();
        _hoveredItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);
        lastRightClickedItem = _hoveredItem;
        if (_hoveredItem != null)
        { 
            rightClickMenu.SetActive(true);
            rightClickMenuSlots.SetActive(false);
            rightClickMenuHolder.transform.position = lastRightClickedItem.transform.position;
        }
        else{
            rightClickMenu.SetActive(false); 
            rightClickMenuSlots.SetActive(false);
            
        }
    }

    Vector2Int GetTileGridPosition(){
        Vector2 position = Input.mousePosition;
        return selectedItemGrid.GetTileGridPosition(position);
    }
    
    Vector2Int GetHighlightTileGridPosition(){
        Vector2 position = Input.mousePosition;
        if (selectedItem != null){
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }
        return selectedItemGrid.GetTileGridPosition(position);
    }

    void PickUpItem(Vector2Int tileGridPosition){
        pickUpPosition = tileGridPosition;
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null){
            _rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    void ItemIconDrag(){
        if (selectedItem != null){
            _rectTransform.position = Input.mousePosition;
        }
    }

    bool SkillPointApplyCheck(){
        foreach (var UIStat in _uiStatsArray){
            if (UIStat.NeedToApplySkills){
                return true;
            }
        }
        return false;
    }

    void PlayInventorySound(){
        _inventoryInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _inventoryInstance.start();  
        }
    }
}
