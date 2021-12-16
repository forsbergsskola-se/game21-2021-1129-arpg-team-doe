using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] public ItemGrid selectedItemGrid;

    void Update(){
        if (selectedItemGrid == null){
            return;
        }

        //Debug.Log(selectedItemGrid.GetTileGridPosition(Input.mousePosition));
    }
}
