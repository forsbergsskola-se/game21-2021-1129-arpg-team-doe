using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
   public string savePath;
   public List<InventorySlot> Container = new List<InventorySlot>();
   ItemDatabaseObject database;

   public void AddItem(ItemObject item, int amount){
      for (int i = 0; i < Container.Count; i++){
         //Here we check if the container already has the item
         if (Container[i].item == item){
            //If we have the item, we add to the amount of that item, instead of adding the item itself
            Container[i].AddAmount(amount);
            return;
         }
      }
      //If we do not have the item already, we add the item
      Container.Add(new InventorySlot(database.GetId[item], item, amount));
   }

   public void Save(){
      string saveData = JsonUtility.ToJson(this, true);
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
      bf.Serialize(file, saveData);
      file.Close();
   }
   
   public void Load(){
      if (File.Exists(string.Concat(Application.persistentDataPath, savePath))){
         BinaryFormatter bf = new BinaryFormatter();
         FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
         JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
         file.Close();
      }
   }

   public void OnAfterDeserialize(){
      for (int i = 0; i < Container.Count; i++){
         Container[i].item = database.GetItem[Container[i].ID];
      }
   }
   
   public void OnBeforeSerialize(){
   }

   void OnEnable(){
#if UNITY_EDITOR
      database = (ItemDatabaseObject) AssetDatabase.LoadAssetAtPath("Assets/Testing area/Resources/Database.asset",
         typeof(ItemDatabaseObject));
#else
      database = Resources.Load<ItemDatabaseObject>("Database");
#endif
   }
}

[System.Serializable]
public class InventorySlot{
   public int ID;
   public ItemObject item;
   [Min(0)]public int amount;

   public InventorySlot(int id, ItemObject item, int amount){
      ID = id;
      this.item = item;
      this.amount = amount;
   }

   public void AddAmount(int value){
      amount += value;
   }
}
