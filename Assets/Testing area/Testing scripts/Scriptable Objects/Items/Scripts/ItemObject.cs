using System;
using System.Collections;
using System.Collections.Generic;
using CustomLogs;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public enum ItemType{
    Consumable,
    Equipment,
    Weapon,
    Currency,
    Default
}

public enum Attributes{
    Toughness,
    Dexterity,
    Knowledge,
    Reflex,
    Luck,
    AttackSpeed,
    Strength
}

public abstract class ItemObject : ScriptableObject{
    
    
    [HideInInspector] public int Id;
    //public GameObject prefab;
    public Sprite itemIcon;
    public int width = 1;
    public int height = 1;
    public ItemType type;
    public string name;
    [Min(0f)] public int price;
    [SerializeField] FMODUnity.EventReference pickupSound ;
    [SerializeField] FMODUnity.EventReference dropSound;
    
    [SerializeField] GameEvent _pickupEvent;
    [TextArea (10,10)] public string description;
    public ItemBuff[] buffs;

    bool hasFoundPickupSound;
    bool hasFoundDropSound;

    EventInstance pickupSoundInstance;
    EventInstance dropSoundInstance;
    public Item CreateItem(){
        Item newItem = new Item(this);
        return newItem;
    }

  

   public virtual void PlayPickupSound()
   {
       if (!hasFoundPickupSound)
       {
           pickupSound = EventReference.Find("event:/PickUp");
           hasFoundPickupSound = true;
       }
       pickupSoundInstance = RuntimeManager.CreateInstance("event:/PickUp");
       //Debug.Log("I am playing pick up sound");
       pickupSoundInstance.start();
       pickupSoundInstance.release();
   }

    public virtual void PlayDropSound()
    {
        if (!hasFoundDropSound)
        {
            dropSound = EventReference.Find("event:/InventoryDrop");
            hasFoundDropSound = true;
        }
        dropSoundInstance = RuntimeManager.CreateInstance("event:/InventoryDrop");
        
        //Debug.Log("I am playing drop sound");
        dropSoundInstance.start();
        dropSoundInstance.release();
    }

    public virtual void UseItem(){
    }
    public void CallEvent(){
        if (_pickupEvent != null){
            _pickupEvent.Invoke();
        }
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item(ItemObject item){
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++){
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int max;
    public int min;
    public ItemBuff(int min, int max){
        this.min = min;
        this.max = max;
        GenerateValue();
    }

    public void GenerateValue(){
        value = UnityEngine.Random.Range(min, max);
    }
}
