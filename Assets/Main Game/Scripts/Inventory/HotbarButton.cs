using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarButton : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] Button button;
    [HideInInspector]
    [SerializeField] TMP_Text text;
    public event Action<int> OnButtonClicked;
    int _keyNumber;
    int _id;
    KeyCode _keyCode;
    InventoryItem _inventoryItem;
    InventoryController _inventoryController;
    Sprite _defaultSprite;
    HotbarButton[] _hotbarButtons;
    void OnValidate(){
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;

        if (text == null){
            text = GetComponentInChildren<TMP_Text>();
        }
        text.SetText(_keyNumber.ToString());
        gameObject.name = "HotBar Button" + _keyNumber;
    }

    void Awake(){
        GetComponent<Button>().onClick.AddListener(HandleClick);
        _inventoryItem = GetComponent<InventoryItem>();
        _inventoryController = FindObjectOfType<InventoryController>();
        button = gameObject.GetComponent<Button>();
        _hotbarButtons = GetComponentInParent<Hotbar>().GetComponentsInChildren<HotbarButton>();
        _id = -1;
        _defaultSprite = button.image.sprite;
    }

    void Update(){
        if (Input.GetKeyDown((_keyCode))){
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
    
    public void AssignButtonButton(){
        var itemObject = _inventoryController.lastRightClickedItem.itemObject;
        foreach (var hotBarButton in _hotbarButtons){
            if (hotBarButton == null){
                continue;
            }
            if (hotBarButton._id == itemObject.Id || itemObject is not ConsumableObject){
                _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
                return;
            }
        }
        _inventoryItem = _inventoryController.lastRightClickedItem;
        if (_inventoryItem == null){
            return;
        }
        button.image.sprite = itemObject.itemIcon;
        _id = itemObject.Id;
        _inventoryController.selectedItem = _inventoryItem;
        _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
    }

    void HandleClick(){
        OnButtonClicked?.Invoke(_keyNumber);
        if (_inventoryItem == null){
            Debug.Log("Hotbar button need to be assigned first");
            return;
        }
        _inventoryController.selectedItem = _inventoryItem;
       Debug.Log($"Item {_id} is used.");
        _inventoryController.UseItem(); // Here can be replaced to a consume function
        ClearButton();
    }

    void ClearButton(){ 
        _inventoryItem = null;
        button.image.sprite = _defaultSprite;
        _id = -1;
    }
}
