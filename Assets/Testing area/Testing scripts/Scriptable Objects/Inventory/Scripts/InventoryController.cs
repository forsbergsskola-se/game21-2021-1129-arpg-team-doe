using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] ItemGrid selectedItemGrid;

    void Update(){
        if (selectedItemGrid == null){
            return;
        }

        //Debug.Log(selectedItemGrid.GetTileGridPosition(Input.mousePosition));
    }
}
