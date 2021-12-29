using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteractHotBar : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    InventoryController _inventoryController;
    ItemGrid _itemGrid;

    void Awake(){
        _inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        _itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        _inventoryController.SelectedItemGrid = _itemGrid;
    }
    
    public void OnPointerExit(PointerEventData eventData){
        _inventoryController.SelectedItemGrid = null;
    }
}
