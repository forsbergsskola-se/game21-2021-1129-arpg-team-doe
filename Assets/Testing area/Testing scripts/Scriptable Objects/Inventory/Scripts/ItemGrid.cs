using UnityEngine;
using UnityEngine.PlayerLoop;

public class ItemGrid : MonoBehaviour
{
    [SerializeField] int gridSizeWidth = 5;
    [SerializeField] int gridSizeHeight = 10;
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;
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

    public Vector2Int GetTileGridPosition(Vector2 mousePosition){
        positionOnTheGrid.x = mousePosition.x - _rectTransform.position.x;
        positionOnTheGrid.y = _rectTransform.position.y - mousePosition.y;
        tileGridPosition.x = (int) (positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int) (positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }
}
