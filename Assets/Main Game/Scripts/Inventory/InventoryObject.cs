using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
   //TODO: Maybe remove this script??
   public string savePath;
   public ItemDatabaseObject database;
   public Inventory Container;

   public void AddItem(Item item, int amount){
      if (item.buffs.Length > 0){
         SetEmptySlot(item, amount);
         return;
      }
      
      for (int i = 0; i < Container.Items.Length; i++){
         //Here we check if the container already has the item
         if (Container.Items[i].ID == item.Id){
            //If we have the item, we add to the amount of that item, instead of adding the item itself
            Container.Items[i].AddAmount(amount);
            return;
         }
      }
      SetEmptySlot(item, amount);
   }

   public InventorySlot SetEmptySlot(Item item,int amount){
      for (int i = 0; i < Container.Items.Length; i++){
         if (Container.Items[i].ID <= -1){
            Container.Items[i].UpdateSlot(item.Id, item, amount);
            return Container.Items[i];
         }
      }
      //set up function for full inventory
      return null;
   }
   
   // Player is able to edit the file
   // public void Save(){
   //    string saveData = JsonUtility.ToJson(this, true);
   //    BinaryFormatter bf = new BinaryFormatter();
   //    FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
   //    bf.Serialize(file, saveData);
   //    file.Close();
   // }
   // public void Load(){
   //    if (File.Exists(string.Concat(Application.persistentDataPath, savePath))){
   //       BinaryFormatter bf = new BinaryFormatter();
   //       FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
   //       JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
   //       file.Close();
   //    }
   // }

   
   public void MoveItem(InventorySlot item1, InventorySlot item2){
      InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
      item2.UpdateSlot(item1.ID, item1.item, item1.amount);
      item1.UpdateSlot(temp.ID, temp.item, temp.amount);
   }

   public void RemoveItem(Item item){
      for (int i = 0; i < Container.Items.Length; i++){
         if (Container.Items[i].item == item){
            Container.Items[i].UpdateSlot(-1,null,0);
         }
      }
   }
   
   [ContextMenu("Save")]
   public void Save(){
      IFormatter formatter = new BinaryFormatter();
      Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,
         FileAccess.Write);
      formatter.Serialize(stream, Container);
      stream.Close();
   }

   [ContextMenu("Load")]
   public void Load(){
      if (File.Exists(string.Concat(Application.persistentDataPath, savePath))){
         IFormatter formatter = new BinaryFormatter();
         Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,
            FileAccess.Read);
         Inventory newContainer = (Inventory) formatter.Deserialize(stream);
         for (int i = 0; i < Container.Items.Length; i++){
            Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount );
         }
         stream.Close();
      }
   }

   [ContextMenu("Clear")]
   public void Clear(){
      Container = new Inventory();
   }

//    void OnEnable(){
// #if UNITY_EDITOR
//       database = (ItemDatabaseObject) AssetDatabase.LoadAssetAtPath("Assets/Testing area/Resources/Database.asset",
//          typeof(ItemDatabaseObject));
// #else
//       database = Resources.Load<ItemDatabaseObject>("Database");
// #endif
//    }
}

[System.Serializable]
public class Inventory{
   public InventorySlot[] Items = new InventorySlot[25];
}

[System.Serializable]
public class InventorySlot{
   public int ID = -1;
   public Item item;
   [Min(0)]public int amount;

   public InventorySlot(){
      ID = -1;
      this.item = null;
      this.amount = 0;
   }
   
   public InventorySlot(int id, Item item, int amount){
      ID = id;
      this.item = item;
      this.amount = amount;
   }
   
   public void UpdateSlot(int id, Item item, int amount){
      ID = id;
      this.item = item;
      this.amount = amount;
   }

   public void AddAmount(int value){
      amount += value;
   }
}
