using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController _inventoryController;

    void Awake(){
        _inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("Pointer enter");
    }

    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("Pointer exit");
    }
}
