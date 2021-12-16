using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public ItemGrid selectedItemGrid;

    InventoryItem _selectedItem;

    void Update(){
        if (selectedItemGrid == null){
            return;
        }

        if (Input.GetMouseButtonDown(0)){
            Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(Input.mousePosition);
            if (_selectedItem == null){
                _selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            }
            else{
                selectedItemGrid.PlaceItem(_selectedItem, tileGridPosition.x, tileGridPosition.y);
                _selectedItem = null;
            }
        }
    }
}
