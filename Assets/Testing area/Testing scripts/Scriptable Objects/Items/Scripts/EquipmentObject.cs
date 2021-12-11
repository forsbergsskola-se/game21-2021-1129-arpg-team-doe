using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
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
