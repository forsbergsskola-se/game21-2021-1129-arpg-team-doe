using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    InventoryController _inventoryController;
    ItemGrid _itemGrid;

    void Awake(){
        _inventoryController = FindObjectOfType<InventoryController>();
        _itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        _inventoryController.SelectedItemGrid = _itemGrid;
        //_inventoryController.hotBarSelected = true;
    }
    
    public void OnPointerExit(PointerEventData eventData){
        //_inventoryController.hotBarSelected = false;
    }
}
