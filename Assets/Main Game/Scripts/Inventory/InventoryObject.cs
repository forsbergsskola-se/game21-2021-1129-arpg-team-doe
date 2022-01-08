using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public InventoryContainer Container;
    public void AddItem(ItemObject item, int amount){
        for (int i = 0; i < Container.inventoryItems.Length; i++){
            if (Container.inventoryItems[i].ID == item.Id){
                Container.inventoryItems[i].AddAmount(amount);
                return;
            }
        }
        SetEmptySlot(item, amount);
    }

    void SetEmptySlot(ItemObject item,int amount){
        for (int i = 0; i < Container.inventoryItems.Length; i++){
            if (Container.inventoryItems[i].ID <= -1){
                Container.inventoryItems[i].UpdateSlot(item.Id, item, amount);
                return;
            }
        }
    }

    public void RemoveItem(ItemObject item){
        for (int i = 0; i < Container.inventoryItems.Length; i++){
            if (Container.inventoryItems[i].itemObject == item){
                Container.inventoryItems[i].AddAmount(-1);
                if (Container.inventoryItems[i].amount <= 0){
                    Container.inventoryItems[i].UpdateSlot(-1,null,0);
                }
                return;
            }
        }
    }

    public int GetItemAmount(ItemObject item){
        int itemAmount = -1;
        for (int i = 0; i < Container.inventoryItems.Length; i++){
            if (Container.inventoryItems[i].itemObject == item){
                itemAmount = Container.inventoryItems[i].amount;
            }
        }
        return itemAmount;
    }
    
    public bool ItemExistInContainer(ItemObject item){
        for (int i = 0; i < Container.inventoryItems.Length; i++){
            if (Container.inventoryItems[i].itemObject == item){
                return true;
            }
        }
        return false;
    }

    [ContextMenu("Clear")]
    public void Clear(){
        Container = new InventoryContainer();
    }
}

[System.Serializable]
public class InventoryContainer{
    public InventorySlots[] inventoryItems = new InventorySlots[20];
}

[System.Serializable]
public class InventorySlots{
    public int ID = -1;
    public ItemObject itemObject;
    [Min(0)]public int amount;

    public InventorySlots(){
        ID = -1;
        itemObject = null;
        amount = 0;
    }

    public void UpdateSlot(int id, ItemObject item, int amount){
        ID = id;
        itemObject = item;
        this.amount = amount;
    }

    public void AddAmount(int value){
        amount += value;
    }
}