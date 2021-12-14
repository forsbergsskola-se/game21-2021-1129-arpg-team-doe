using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
   public string savePath;
   public ItemDatabaseObject database;
   public Inventory Container;

   public void AddItem(Item item, int amount){
      for (int i = 0; i < Container.Items.Count; i++){
         //Here we check if the container already has the item
         if (Container.Items[i].item.Id == item.Id){
            //If we have the item, we add to the amount of that item, instead of adding the item itself
            Container.Items[i].AddAmount(amount);
            return;
         }
      }
      //If we do not have the item already, we add the item
      Container.Items.Add(new InventorySlot(item.Id, item, amount));
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
         Container = (Inventory) formatter.Deserialize(stream);
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
public class Inventory
{
   public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot{
   public int ID;
   public Item item;
   [Min(0)]public int amount;

   public InventorySlot(int id, Item item, int amount){
      ID = id;
      this.item = item;
      this.amount = amount;
   }

   public void AddAmount(int value){
      amount += value;
   }
}
