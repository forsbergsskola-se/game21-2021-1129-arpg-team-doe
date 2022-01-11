using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarButton : MonoBehaviour{
    [HideInInspector]
    [SerializeField] Button button;
    [HideInInspector]
    [SerializeField] TMP_Text text;
    
    public event Action<int> OnButtonClicked;
    int _keyNumber;
    int _id;
    KeyCode _keyCode;
    [HideInInspector] public InventoryItem _inventoryItem;
    InventoryController _inventoryController;
    Sprite _defaultSprite;
    [HideInInspector] public HotbarButton[] _hotbarButtons;

    void Awake(){
        GetComponent<Button>().onClick.AddListener(HandleClick);
        _inventoryItem = GetComponent<InventoryItem>();
        _inventoryController = FindObjectOfType<InventoryController>();
        button = gameObject.GetComponent<Button>();
        _hotbarButtons = GetComponentInParent<Hotbar>().GetComponentsInChildren<HotbarButton>();
    }

    void Start(){
        _id = -1;
        _defaultSprite = button.image.sprite;
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;

        if (text == null){
            text = GetComponentInChildren<TMP_Text>();
        }
        text.SetText(_keyNumber.ToString());
        gameObject.name = "HotBar Button" + _keyNumber;
    }

    void Update(){
        if (Input.GetKeyDown(_keyCode)){
            if (Input.GetKey(KeyCode.LeftAlt) && _inventoryController.selectedItem != null){
                AssignButton();
            }
            else if(!Input.GetKey(KeyCode.LeftAlt)){
                HandleClick();
            }
        }
    }

    void AssignButton(){
        var itemObject = _inventoryController.selectedItem.itemObject;
        foreach (var hotBarButton in _hotbarButtons){
            if (hotBarButton == null){
                continue;
            }
            if (hotBarButton._id == itemObject.Id || itemObject is not ConsumableObject){
                _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
                return;
            }
        }
        _inventoryItem = _inventoryController.selectedItem;
        if (_inventoryItem == null){
            return;
        }
        button.image.sprite = itemObject.itemIcon;
        _id = itemObject.Id;
        _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
        
        if (_inventoryController.selectedItemGrid.IsOutOfInventoryGrid(_inventoryController.pickUpPosition.x,
            _inventoryController.pickUpPosition.y)){
            ClearButton();
        }
    }
    
    public void RightClickAssignButton(){
        var itemObject = _inventoryController.lastRightClickedItem.itemObject;
        foreach (var hotBarButton in _hotbarButtons){
            if (hotBarButton == null){
                continue;
            }
            if (hotBarButton._id == itemObject.Id || itemObject is not ConsumableObject){
                _inventoryController.lastRightClickedItem = null;
                return;
            }
        }
        _inventoryItem = _inventoryController.lastRightClickedItem;
        if (_inventoryItem == null){
            return;
        }
        button.image.sprite = itemObject.itemIcon;
        _id = itemObject.Id;
        _inventoryController.PlaceItemBackWhenButtonAssigned(_inventoryController.pickUpRightClickPosition);
    }

    void HandleClick(){
        OnButtonClicked?.Invoke(_keyNumber);
        if (_inventoryItem == null){
            ClearButton();
            return;
        }
        _inventoryController.selectedItem = _inventoryItem;
        _inventoryController.UseItem();
        ClearButton();
    }

    public void ClearButton(){ 
        _inventoryItem = null;
        button.image.sprite = _defaultSprite;
        _id = -1;
    }
}