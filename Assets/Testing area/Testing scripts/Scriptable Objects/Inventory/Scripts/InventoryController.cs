using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

// This script is attached to the camera to get mouse position for item placement
public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid{
        get => selectedItemGrid;
        set{
            selectedItemGrid = value;
            _inventoryHighlight.SetParent(value);
        }
    }

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryItem _selectedItem;
    InventoryItem _overlapItem;
    RectTransform _rectTransform;
    InventoryHighlight _inventoryHighlight;
    InventoryItem _itemToHighligt;
    Vector2Int oldPosition;
    bool _clickOnInventory;

    void Awake(){
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }


    void Update(){
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q)){ //debug version
            if (_selectedItem == null){
                CreateRandomItem();
            }
        }

        //add item to inventory
        if (Input.GetKeyDown(KeyCode.W)){ //debug version
            InsertRandomItem();
        }

        //rotate items
        if (Input.GetKeyDown(KeyCode.R)){
            RotateItem(); 
        }

        if (selectedItemGrid == null){
            _inventoryHighlight.Show(false);
            _clickOnInventory = false;
            //return;
        }
        else{
            _clickOnInventory = true;
        }

        if (_clickOnInventory){
            HandleHighlight();
            if (Input.GetMouseButtonDown(0)){
                LeftMouseButtonPress();
            }
        }
        
        if (!_clickOnInventory && Input.GetMouseButtonDown(0) && _selectedItem != null){
            DropItemToGround();
        }
    }

    void DropItemToGround(){
        RemoveItem();
    }

    void RotateItem(){
        if (_selectedItem == null){
            return;
        }

        _selectedItem.Rotate();
    }

    void InsertRandomItem(){
        if (selectedItemGrid == null){
            return;
        }
        
        CreateRandomItem();
        InventoryItem itemToInsert = _selectedItem;
        _selectedItem = null;
        InsertItem(itemToInsert);
    }

    void InsertItem(InventoryItem itemToInsert){
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null){
            return;
        }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }


    void HandleHighlight(){
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (oldPosition == positionOnGrid){
            return;
        }
        oldPosition = positionOnGrid;

        //if(oldPosition != positionOnGrid)
        if (_selectedItem == null){
            _itemToHighligt = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (_itemToHighligt != null){
                _inventoryHighlight.Show(true);
                _inventoryHighlight.SetSize(_itemToHighligt);
                //_inventoryHighlight.SetParent(selectedItemGrid);
                _inventoryHighlight.SetPosition(selectedItemGrid, _itemToHighligt);
            }
            else{
                _inventoryHighlight.Show(false);
            }
        }
        else{
            {
                _inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, 
                    _selectedItem.WIDTH, _selectedItem.HEIGHT));
                _inventoryHighlight.SetSize(_selectedItem);
                //_inventoryHighlight.SetParent(selectedItemGrid);
                _inventoryHighlight.SetPosition(selectedItemGrid, _selectedItem, positionOnGrid.x, 
                    positionOnGrid.y);
            }
        }
    }

    void CreateRandomItem(){
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        _selectedItem = inventoryItem;
        
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvasTransform);
        _rectTransform.SetAsLastSibling(); //item you hold dont get behind a placed item in inventory

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }
    
    void RemoveItem(){
        _selectedItem.gameObject.SetActive(false);
        _selectedItem = null;
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
            position.x -= (_selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (_selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }
        //Debug.Log(position);
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
                _rectTransform.SetAsLastSibling(); //item you hold dont get behind a placed item in inventory
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
