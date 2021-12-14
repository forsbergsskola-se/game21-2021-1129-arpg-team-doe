using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Consumable,
    Equipment,
    Weapon,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    //public GameObject prefab;
    public Sprite uiDisplay;
    public ItemType type;
    public string name;
    [Min(0f)] public float price;
    [Tooltip("Weight in kg")][Min(0f)] public float weight;
    [TextArea (10,10)] public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(ItemObject item){
        Name = item.name;
        Id = item.Id;
    }
}
