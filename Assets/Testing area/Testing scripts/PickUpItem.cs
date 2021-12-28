using UnityEngine;

public class PickUpItem : MonoBehaviour, Iinteractable{

    InventoryController _inventoryController;

    bool _used;
    
    void Start(){
        _inventoryController = FindObjectOfType<InventoryController>();
    }
    
    public void Use(){
        if (!_used){
            int id = gameObject.GetComponent<InventoryItem>().itemObject.Id;
            //_inventoryController.InsertItem(_inventoryController.CreateRandomItem());
            _inventoryController.InsertItem(_inventoryController.CreateItem(id));
            _used = true;
            Destroy(gameObject);
        }
    }
}
