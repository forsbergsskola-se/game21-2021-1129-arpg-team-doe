using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Consumable,
    Equipment,
    Default
}


public abstract class ItemObject : ScriptableObject{
    public GameObject prefab;
    public ItemType type;
    public string name;
    [Min(0f)] public float price;
   [Tooltip("Weight in kg")][Min(0f)] public float weight;
    [TextArea (10,10)] public string description;
    
}
