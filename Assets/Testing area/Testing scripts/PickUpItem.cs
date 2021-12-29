using UnityEngine;

public class PickUpItem : MonoBehaviour, Iinteractable{

    InventoryController _inventoryController;

    bool _used;

    FMODUnity.EventReference fmodEvent;
    FMOD.Studio.EventInstance instance;
    
    void Start(){
        _inventoryController = FindObjectOfType<InventoryController>();
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/PickUp"); //works in a hardcoded fashion.. sorry!
    }

    public void Use(){
        if (!_used){
            int id = gameObject.GetComponent<InventoryItem>().itemObject.Id;
            //_inventoryController.InsertItem(_inventoryController.CreateRandomItem());
            _inventoryController.InsertItem(_inventoryController.CreateItem(id));
            _used = true;
            PlayPickupSound();
            Destroy(gameObject);
        }
    }

    public void PlayPickupSound(){
        instance.start();
        instance.release();
    }
}