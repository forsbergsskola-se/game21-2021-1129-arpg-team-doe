using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject{
   public List<InventorySlot> Container = new List<InventorySlot>();

   public void AddItem(ItemObject _item, int _amount){

      bool hasItem = false;
      for (int i = 0; i < Container.Count; i++){
         //Here we check if the container already has the item
         if (Container[i].item == _item){
            //If we have the item, we add to the amount of that item, instead of adding the item itself
            Container[i].AddAmount(_amount);
            hasItem = true;
            break;
         }
      }

      if (!hasItem){
         //If we do not have the item already, we add the item
         Container.Add(new InventorySlot(_item,_amount));
      }
   }
}

[System.Serializable]
public class InventorySlot{
   public ItemObject item;
   [Min(0)]public int amount;

   public InventorySlot(ItemObject _item, int _amount){
      item = _item;
      amount = _amount;
   }

   public void AddAmount(int value){
      amount += value;
   }
}
