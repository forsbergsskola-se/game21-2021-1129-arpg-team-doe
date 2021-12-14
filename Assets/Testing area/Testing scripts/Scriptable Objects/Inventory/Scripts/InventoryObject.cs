using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver{
   public ItemDatabaseObject database;
   public List<InventorySlot> Container = new List<InventorySlot>();
   public void AddItem(ItemObject _item, int _amount){
      for (int i = 0; i < Container.Count; i++){
         //Here we check if the container already has the item
         if (Container[i].item == _item){
            //If we have the item, we add to the amount of that item, instead of adding the item itself
            Container[i].AddAmount(_amount);
            return;
         }
      }
      //If we do not have the item already, we add the item
      Container.Add(new InventorySlot(database.GetId[_item], _item,_amount));
   }

   public void OnBeforeSerialize(){
   }

   public void OnAfterDeserialize(){
      for (int i = 0; i < Container.Count; i++){
         Container[i].item = database.GetItem[Container[i].ID];
      }
   }
}

[System.Serializable]
public class InventorySlot{
   public int ID;
   public ItemObject item;
   [Min(0)]public int amount;

   public InventorySlot(int _id, ItemObject _item, int _amount){
      item = _item;
      amount = _amount;
   }

   public void AddAmount(int value){
      amount += value;
   }
}
