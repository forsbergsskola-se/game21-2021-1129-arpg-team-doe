using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public ItemGrid selectedItemGrid;

    void Update(){
        if (selectedItemGrid == null){
            return;
        }

        if (Input.GetMouseButtonDown(0)){
            Debug.Log(selectedItemGrid.GetTileGridPosition(Input.mousePosition));
        }
    }
}
