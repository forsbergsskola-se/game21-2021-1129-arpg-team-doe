using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotBarButton : MonoBehaviour
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
    HotBarButton[] _hotBarButtons;
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
        _hotBarButtons = GetComponentInParent<HotBar>().GetComponentsInChildren<HotBarButton>();
        _id = -1;
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

    public void AssignButton(){
        _defaultSprite = button.image.sprite;
        ItemObject itemObject = _inventoryController.selectedItem.itemObject;
        foreach (var hotBarButton in _hotBarButtons){
            if (hotBarButton != null){
                if (hotBarButton._id == itemObject.Id || itemObject is not ConsumableObject){
                    _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
                    return;
                }
                if (hotBarButton._id != itemObject.Id){
                    _inventoryItem = _inventoryController.selectedItem;
                    if (_inventoryItem == null){
                        return;
                    }
                    button.image.sprite = _inventoryItem.itemObject.itemIcon;
                    _id = itemObject.Id;
                    //Debug.Log(_inventoryController.pickUpPosition); // pick up position need to be recalculated
                    _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
                    return;
                }
            }
        }
    }

    void HandleClick(){
        OnButtonClicked?.Invoke(_keyNumber);
        if (_inventoryItem == null){
            Debug.Log("HotBar button need to be assigned first");
            return;
        }
        _inventoryController.selectedItem = _inventoryItem;
        Debug.Log($"Item {_id} is used.");
        _inventoryController.DropItemToGround(); // Here can be replaced to a consume function
        ClearButton();
    }

    void ClearButton(){ //TODO: clear multiple button
        _inventoryItem = null;
        button.image.sprite = _defaultSprite;
    }
}
