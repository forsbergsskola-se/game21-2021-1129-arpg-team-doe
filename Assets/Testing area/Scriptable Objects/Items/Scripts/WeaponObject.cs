using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]

public class WeaponObject :ItemObject
{
    [Header("Weapon Stats")]
    [Min(0)][Tooltip("The equipment's attackspeed")] 
    public int attackSpeed;
    [Min(0)][Tooltip("The equipment's damage")] 
    public int damage;
    [Min(0)][Tooltip("Amount of attacks per second Buff")] 
    public int attackSpeedBuff;
    [Tooltip("The type of damage this weapon does")]
    public DamageType damageType;
   
    [Header("Attribute Buffs")]
    public int toughnessBuff;
    public int strengthBuff;
    public int dexterityBuff;
    public int knowledgeBuff;
    public int luckBuff;
    

   public void Awake(){
        type = ItemType.Weapon;
    }
}
