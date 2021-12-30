using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarButton : MonoBehaviour
{
    [SerializeField] Button button; //
    [SerializeField] TMP_Text text;
    public event Action<int> OnButtonClicked;
    int _keyNumber;
    KeyCode _keyCode;
    InventoryItem _inventoryItem;
    InventoryController _inventoryController;

    void OnValidate(){
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;

        if (text == null){
            text = GetComponentInChildren<TMP_Text>();
        }
        text.SetText(_keyNumber.ToString());
        gameObject.name = "Hotbar Button" + _keyNumber;
    }

    void Awake(){
        GetComponent<Button>().onClick.AddListener(HandleClick);
        _inventoryItem = GetComponent<InventoryItem>();
        _inventoryController = FindObjectOfType<InventoryController>();
        button = gameObject.GetComponent<Button>();
    }

    void Update(){
        if (Input.GetKeyDown((_keyCode))){
            if (Input.GetKey(KeyCode.LeftAlt)){
                AssignButton();
            }
            else{
                HandleClick();
            }
        }
    }

    void AssignButton(){
        _inventoryItem = _inventoryController.selectedItem;
        if (_inventoryItem == null){
            return;
        }
        button.image.sprite = _inventoryItem.itemObject.itemIcon;
        _inventoryController.PlaceItem(_inventoryController.pickUpPosition);
    }

    void HandleClick(){
        OnButtonClicked?.Invoke(_keyNumber);
    }
}
