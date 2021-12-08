using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public GameObject equipmentPrefab;
    public string equipmentName;
    public int toughnessBuff;
    public int strengthBuff;
    public int dexterityBuff;
    public int knowledgeBuff;
    public int luckBuff;
    [Tooltip("Amount of attacks per second Buff")] public int attackSpeedBuff;
   [Tooltip("The equipment's attackspeed")] public int attackSpeed;
   [Tooltip("The equipment's damage")] public int damage;
    

   public void Awake(){
        type = ItemType.Equipment;
    }
}
