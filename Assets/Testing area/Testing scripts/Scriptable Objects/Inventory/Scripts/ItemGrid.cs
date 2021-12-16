using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float tileSizeWidth = 32;
    const float tileSizeHeight = 32;
    RectTransform _rectTransform;

    void Start(){
        _rectTransform = GetComponent<RectTransform>();
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
