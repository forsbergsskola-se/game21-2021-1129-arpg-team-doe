using UnityEngine;

public class ItemGrid : MonoBehaviour{
    [SerializeField] int gridSizeWidth = 10;
    [SerializeField] int gridSizeHeight = 10;
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;
    Vector2 _positionOnTheGrid;
    Vector2Int _tileGridPosition;
    InventoryItem[,] _inventoryItemSlot;
    RectTransform _rectTransform;

    void Start(){
        _rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    void Init(int width, int height){
        _inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        _rectTransform.sizeDelta = size;
    }

    public InventoryItem PickUpItem(int x, int y){
        if (IsOutOfInventoryGrid(x, y)){
            return null;
        }
        
        InventoryItem itemToReturn = _inventoryItemSlot[x, y];

        if (itemToReturn == null){
            return null;
        }

        itemToReturn.itemObject.PlayPickupSound();
        CleanGridReference(itemToReturn);
        return itemToReturn;
    }

    public bool IsOutOfInventoryGrid(int x, int y){
        Vector2 inventorySize = _rectTransform.sizeDelta;
        return x < 0 || x >= inventorySize.x / tileSizeWidth || y < 0 || y >= inventorySize.y / tileSizeHeight;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition){
        Vector2 rectPosition = _rectTransform.position;
        _positionOnTheGrid.x = mousePosition.x - rectPosition.x;
        _positionOnTheGrid.y = rectPosition.y - mousePosition.y;
        _tileGridPosition.x = (int) (_positionOnTheGrid.x / tileSizeWidth);
        _tileGridPosition.y = (int) (_positionOnTheGrid.y / tileSizeHeight);

        return _tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem _overlapItem){
        Debug.Log(inventoryItem);
        if (BoundaryCheck(posX,posY,inventoryItem.WIDTH,inventoryItem.HEIGHT) == false){
            return false;
        }

        if (OverlapCheck(posX,posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref _overlapItem) == false){
            _overlapItem = null;
            return false;
        }

        if (_overlapItem != null){
            CleanGridReference(_overlapItem);
        }
        
        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY){
        
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(_rectTransform);

        for (int x = 0; x < inventoryItem.WIDTH; x++){
            for (int y = 0; y < inventoryItem.HEIGHT; y++){
                _inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
        inventoryItem.itemObject.PlayDropSound();
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY){
        Vector2 position;
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH/ 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
    }
    
    void CleanGridReference(InventoryItem item){
        for (int ix = 0; ix < item.WIDTH; ix++){
            for (int iy = 0; iy < item.HEIGHT; iy++){
                _inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }
    
    internal InventoryItem GetItem(int x, int y){
        if (IsOutOfInventoryGrid(x, y)){
            return null;
        }
        return _inventoryItemSlot[x, y];
    }

    bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem){
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                if (_inventoryItemSlot[posX+x, posY + y] != null){
                    if (overlapItem == null){
                        overlapItem = _inventoryItemSlot[posX + x, posY + y];
                    }
                    else{
                        if (overlapItem != _inventoryItemSlot[posX + x, posY + y]){
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    
    bool CheckAvailableSpace(int posX, int posY, int width, int height){
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                if (_inventoryItemSlot[posX+x, posY + y] != null){
                    return false;
                }
            }
        }
        return true;
    }

    bool PositionCheck(int posX, int posY){
        if (posX < 0 || posY < 0){
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight){
            return false;
        }

        return true;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height){
        if (PositionCheck(posX,posY) == false){
            return false;
        }

        posX += width -1;
        posY += height -1;

        if (PositionCheck(posX,posY) == false){
            return false;
        }
        
        return true;
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert){
        int height = gridSizeHeight - itemToInsert.HEIGHT + 1;
        int width = gridSizeWidth - itemToInsert.WIDTH + 1;
        
        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                if (CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT)){
                    return new Vector2Int(x, y);
                }
            }
        }
        return null;
    }
}
