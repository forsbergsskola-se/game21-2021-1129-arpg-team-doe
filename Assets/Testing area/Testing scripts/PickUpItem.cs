using UnityEngine;

public class PickUpItem : MonoBehaviour, Iinteractable{

    InventoryController _inventoryController;

    bool _used;

    void Start(){
        _inventoryController = FindObjectOfType<InventoryController>();
    }

    public void Use(){
        if (!_used){
            var itemObject = gameObject.GetComponent<InventoryItem>().itemObject;
            //_inventoryController.InsertItem(_inventoryController.CreateRandomItem());
            _inventoryController.InsertItem(_inventoryController.CreateItem(itemObject.Id));
            _used = true;
            itemObject.PlayPickupSound();
            Destroy(gameObject);
        }
    }
}