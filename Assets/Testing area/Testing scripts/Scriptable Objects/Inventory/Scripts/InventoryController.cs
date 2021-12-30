using System;
using System.Collections.Generic;
using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

// This script is attached to the camera to get mouse position for item placement
public class InventoryController : MonoBehaviour
{
    public GameObject canvasInventory;
    //[HideInInspector]
    public ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid{
        get => selectedItemGrid;
        set{
            selectedItemGrid = value;
            _inventoryHighlight.SetParent(value);
        }
    }

    [SerializeField] ItemDatabaseObject itemsInDatabase;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    [SerializeField] GameObject[] groundItemPrefabs;

    public GameObject DroppedObject{ get; private set; }
    public InventoryItem selectedItem;
    public Vector2Int pickUpPosition;

    InventoryItem _overlapItem;
    InventoryItem _hoveredItem;
    RectTransform _rectTransform;
    InventoryHighlight _inventoryHighlight;
    InventoryItem _itemToHighlight;
    Transform _playerTransform;
    Consumer _playerConsumer;
    Vector2Int _oldPosition;
    UIStats[] UIStatsArray;
    EventInstance _inventoryInstance;
    public FMODUnity.EventReference inventoryReference;
    //public bool hotBarSelected;
    
    bool _clickOnInventory;

    void Awake(){
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    void Start(){
        
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerConsumer = _playerTransform.GetComponent<Consumer>();
         UIStatsArray = FindObjectsOfType<UIStats>();
         _inventoryInstance = FMODUnity.RuntimeManager.CreateInstance(inventoryReference);
    }

    void Update(){
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.I) && !SkillPointApplyCheck()){
            ToggleInventory();
            PlayInventorySound();
        }

        if (Input.GetKeyDown(KeyCode.Q)){ //debug version
            if (selectedItem == null){
                CreateRandomItem();
            }
        }

        //add item to inventory
        //if (Input.GetKeyDown(KeyCode.W)){ //debug version
        //    InsertRandomItem();
        //}

        //rotate items
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
        
        MouseOver();
        
       
        //if mouse over, display item object stats etc. _selecteditem.itemobject.whatever

        if (Input.GetMouseButtonDown(1)){
            RightMouseButtonPress();
        }
    }
    void MouseOver()
    {
        var tileGridPosition = GetTileGridPosition();
        _hoveredItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);
        
        if (_hoveredItem != null){
           // _hoveredItem.itemObject.DisplayItem(); // Here we want to display the item information in the game view
        }
    }
    public void InsertItem(InventoryItem itemToInsert){
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null){
            return;
        }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }
    
    public InventoryItem CreateRandomItem(){
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        //_selectedItem = inventoryItem;
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvasTransform);
        _rectTransform.SetAsLastSibling(); //item you hold dont get behind a placed item in inventory

        int selectedItemID = UnityEngine.Random.Range(0, itemsInDatabase.GetItem.Count);
        inventoryItem.Set(itemsInDatabase.GetItem[selectedItemID]);
        return inventoryItem;
    }
    
    public InventoryItem CreateItem(int selectedItemID){
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvasTransform);
        _rectTransform.SetAsLastSibling();
        inventoryItem.Set(itemsInDatabase.GetItem[selectedItemID]);

        return inventoryItem;
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

    void InsertRandomItem(){
        if (selectedItemGrid == null){
            return;
        }
        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem ;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    void HandleHighlight(){
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (_oldPosition == positionOnGrid){
            return;
        }
        _oldPosition = positionOnGrid;

        //if(oldPosition != positionOnGrid)
        if (selectedItem == null){
            _itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (_itemToHighlight != null){
                _inventoryHighlight.Show(true);
                _inventoryHighlight.SetSize(_itemToHighlight);
                //_inventoryHighlight.SetParent(selectedItemGrid);
                _inventoryHighlight.SetPosition(selectedItemGrid, _itemToHighlight);
            }
            else{
                _inventoryHighlight.Show(false);
            }
        }
        else{
            {
                _inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, 
                    selectedItem.WIDTH, selectedItem.HEIGHT));
                _inventoryHighlight.SetSize(selectedItem);
                //_inventoryHighlight.SetParent(selectedItemGrid);
                _inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, 
                    positionOnGrid.y);
            }
        }
    }

    void RemoveItemFromInventory(){
        Destroy(selectedItem.gameObject);
        selectedItem = null;
    }
    
    void SpawnItemOnGround(){
        var droppedItem = groundItemPrefabs[selectedItem.itemObject.Id];
        Vector3 spawnPosition = _playerTransform.position + new Vector3(0, 0, 2);
        DroppedObject = Instantiate(droppedItem, spawnPosition, Quaternion.identity);
        DroppedObject.name = droppedItem.name;
    }

    void LeftMouseButtonPress(){
        var tileGridPosition = GetTileGridPosition();

        if (selectedItem == null){
            PickUpItem(tileGridPosition);
        }
        else{
            PlaceItem(tileGridPosition);
        }
    }

    void RightMouseButtonPress(){
        var tileGridPosition = GetTileGridPosition();
        _hoveredItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);
        if (_hoveredItem != null){
            if (_hoveredItem.itemObject is ConsumableObject)
            {
              
                _playerConsumer._consumableObject = (ConsumableObject)_hoveredItem.itemObject;
                _playerConsumer.Consume();
                _playerConsumer._consumableObject = null;
            }
            else
            {
               _hoveredItem.itemObject.UseItem(); 
            }

        }
        else{
            
        }
    }

    Vector2Int GetTileGridPosition(){
        Vector2 position = Input.mousePosition;
        if (selectedItem != null){
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }
        return selectedItemGrid.GetTileGridPosition(position);
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
        foreach (var UIStat in UIStatsArray){
            if (UIStat.NeedToApplySkills){
                return true;
            }
        }
        return false;
    }
    public void PlayInventorySound(){
        _inventoryInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _inventoryInstance.start();  
        }
    }
}
