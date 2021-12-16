using UnityEngine;
using UnityEngine.PlayerLoop;

public class ItemGrid : MonoBehaviour
{
    [SerializeField] int gridSizeWidth = 20;
    [SerializeField] int gridSizeHeight = 10;
    [SerializeField] GameObject inventoryItemPrefab;
    
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;
    InventoryItem[,] _inventoryItemSlot;
    RectTransform _rectTransform;

    void Start(){
        _rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
        
        InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 0, 0);
        
        inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 2, 3);
        
        inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 3, 4);
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
        _inventoryItemSlot[x, y] = null;
        return toReturn;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition){
        positionOnTheGrid.x = mousePosition.x - _rectTransform.position.x;
        positionOnTheGrid.y = _rectTransform.position.y - mousePosition.y;
        tileGridPosition.x = (int) (positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY){
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this._rectTransform);
        _inventoryItemSlot[posX, posY] = inventoryItem;

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight / 2);

        rectTransform.localPosition = position;
    }
}
