using UnityEngine;
using UnityEngine.PlayerLoop;

public class ItemGrid : MonoBehaviour
{
    [SerializeField] int gridSizeWidth = 10;
    [SerializeField] int gridSizeHeight = 10;

    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;
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

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    public InventoryItem PickUpItem(int x, int y){
        if (IsOutOfInventoryGrid(x, y)){
            return null;
        }
        
        InventoryItem toReturn = _inventoryItemSlot[x, y];

        if (toReturn == null){
            return null;
        }

        CleanGridReference(toReturn);
        return toReturn;
    }

    public bool IsOutOfInventoryGrid(int x, int y){
        Vector2 inventorySize = _rectTransform.sizeDelta;
        return x < 0 || x > inventorySize.x || y < 0 || y > inventorySize.y;
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
        //Debug.Log(new Vector2(x,y));
        return _inventoryItemSlot[x, y];
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition){
        Vector2 rectPosition = _rectTransform.position;
        positionOnTheGrid.x = mousePosition.x - rectPosition.x;
        positionOnTheGrid.y = rectPosition.y - mousePosition.y;
        tileGridPosition.x = (int) (positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem _overlapItem){
        
        if (BoundryCheck(posX,posY,inventoryItem.WIDTH,inventoryItem.HEIGHT) == false){
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
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY){
        Vector2 position;
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH/ 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
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

    public bool BoundryCheck(int posX, int posY, int width, int height){
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
