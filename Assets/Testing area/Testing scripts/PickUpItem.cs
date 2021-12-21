using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, Iinteractable{

    InventoryController _inventoryController;

    bool _used;
    
    void Start(){
        _inventoryController = FindObjectOfType<InventoryController>();
    }
    
    public void Use(){
        if (!_used){
            _inventoryController.InsertItem(_inventoryController.CreateRandomItem());
            Debug.Log("Used");
            _used = true;
            Destroy(this.gameObject);
        }
    }
}
