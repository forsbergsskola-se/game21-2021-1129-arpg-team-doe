using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject{

    public int restoreHealthValue;
   public AudioClip consumeSound; 
    [Header("Attribute Buffs")]
   [Min(0f)][Tooltip	("Duration in Seconds")] public int buffDuration;
   public int toughnessBuff;
   public int strengthBuff;
   public int dexterityBuff;
   public int knowledgeBuff;
   public int luckBuff;
   [Tooltip	("Amount of attacks per second Buff")] public int attackSpeedBuff;
   public int damageBuff;
  
   
   public void Awake(){
      type = ItemType.Consumable;
   }
}
