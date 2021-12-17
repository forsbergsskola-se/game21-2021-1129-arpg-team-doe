using UnityEngine;

// This script is attached to the camera to get mouse position for item placement
public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public ItemGrid selectedItemGrid;
    

    InventoryItem _selectedItem;
    RectTransform _rectTransform;

    void Update(){
        if (_selectedItem != null){
            _rectTransform.position = Input.mousePosition;
        }
        
        if (selectedItemGrid == null){
            return;
        }
        
        if (Input.GetMouseButtonDown(0)){
            Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(Input.mousePosition);
            if (_selectedItem == null){
                _selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
                if (_selectedItem != null){
                   _rectTransform = _selectedItem.GetComponent<RectTransform>(); 
                }
                
            }
            else{
                selectedItemGrid.PlaceItem(_selectedItem, tileGridPosition.x, tileGridPosition.y);
                _selectedItem = null;
            }
        }
    }
}
