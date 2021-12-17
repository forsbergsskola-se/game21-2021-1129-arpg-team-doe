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
        InventoryItem toReturn = _inventoryItemSlot[x, y];

        if (toReturn == null){
            return null;
        }

        CleanGridReference(toReturn);
        return toReturn;
    }

    void CleanGridReference(InventoryItem item){
        for (int ix = 0; ix < item.itemData.width; ix++){
            for (int iy = 0; iy < item.itemData.height; iy++){
                _inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }
    
    internal InventoryItem GetItem(int x, int y){
        return _inventoryItemSlot[x, y];
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition){
        positionOnTheGrid.x = mousePosition.x - _rectTransform.position.x;
        positionOnTheGrid.y = _rectTransform.position.y - mousePosition.y;
        tileGridPosition.x = (int) (positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem _overlapItem){
        
        if (BoundryCheck(posX,posY,inventoryItem.itemData.width,inventoryItem.itemData.height) == false){
            return false;
        }

        if (OverlapCheck(posX,posY, inventoryItem.itemData.width, inventoryItem.itemData.height, ref _overlapItem) == false){
            _overlapItem = null;
            return false;
        }

        if (_overlapItem != null){
            CleanGridReference(_overlapItem);
        }
        
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this._rectTransform);
        
        for (int x = 0; x < inventoryItem.itemData.width; x++){
            for (int y = 0; y < inventoryItem.itemData.height; y++){
                _inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);
        rectTransform.localPosition = position;

        return true;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY){
        Vector2 position;
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.itemData.width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.itemData.height / 2);
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

    bool PositionCheck(int posX, int posY){
        if (posX < 0 || posY < 0){
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight){
            return false;
        }

        return true;
    }

    bool BoundryCheck(int posX, int posY, int width, int height){
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
}
