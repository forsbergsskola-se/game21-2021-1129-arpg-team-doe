using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
    Head,
    Chest,
    Accessory,
    Legs,
    Feet
}

public abstract class EquipmentObject : ItemObject
{
    [SerializeField] public EquipmentType equipmentType;
    
    public GameObject equipmentPrefab;
    [Header("Attribute Buffs")]
    public int toughnessBuff;
    public int strengthBuff;
    public int dexterityBuff;
    public int knowledgeBuff;
    public int luckBuff;
    
    public void Awake(){
        type = ItemType.Equipment;
    }
}
