using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

// This script is attached to the camera to get mouse position for item placement
public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public ItemGrid selectedItemGrid;
    
    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvsTransform;

    InventoryItem _selectedItem;
    InventoryItem _overlapItem;
    RectTransform _rectTransform;
    InventoryHighlight _inventoryHighlight;
    InventoryItem _itemToHighligt;

    void Awake(){
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }


    void Update(){
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q)){
            CreateRandomItem();
        }
        
        if (selectedItemGrid == null){
            return;
        }
        
        HandleHighlight();
        
        if (Input.GetMouseButtonDown(0)){
            LeftMouseButtonPress();
        }
    }

    

    void HandleHighlight(){
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (_selectedItem == null){
            _itemToHighligt = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (_itemToHighligt != null){
                _inventoryHighlight.SetSize(_itemToHighligt);
                _inventoryHighlight.SetPosition(selectedItemGrid, _itemToHighligt);
            }
        }
    }

    void CreateRandomItem(){
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        _selectedItem = inventoryItem;
        
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvsTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    void LeftMouseButtonPress(){
        var tileGridPosition = GetTileGridPosition();

        if (_selectedItem == null){
            PickUpItem(tileGridPosition);
        }
        else{
            PlaceItem(tileGridPosition);
        }
    }

    Vector2Int GetTileGridPosition(){
        Vector2 position = Input.mousePosition;

        if (_selectedItem != null){
            position.x -= (_selectedItem.itemData.width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (_selectedItem.itemData.height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    void PlaceItem(Vector2Int tileGridPosition){
        bool complete = selectedItemGrid.PlaceItem(_selectedItem, tileGridPosition.x, tileGridPosition.y, ref _overlapItem);
        if (complete){
            _selectedItem = null;
            if (_overlapItem != null){
                _selectedItem = _overlapItem;
                _overlapItem = null;
                _rectTransform = _selectedItem.GetComponent<RectTransform>();
            }
        }
        
    }

    void PickUpItem(Vector2Int tileGridPosition){
        _selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (_selectedItem != null){
            _rectTransform = _selectedItem.GetComponent<RectTransform>();
        }
    }

    void ItemIconDrag(){
        if (_selectedItem != null){
            _rectTransform.position = Input.mousePosition;
        }
    }
}
