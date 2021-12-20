using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject, IConsumable{
    [SerializeField] GameEvent _consumeEvent;

    public int restoreHealthValue;
    public FMODUnity.EventReference fmodEvent;
    [Header("Attribute Buffs")]
   [Min(0f)][Tooltip	("Duration in Seconds")] public int buffDuration;
   public int toughnessBuff;
   public int strengthBuff;
   public int dexterityBuff;
   public int knowledgeBuff;
   public int luckBuff;
   [Tooltip	("Amount of attacks per second Buff")] public int attackSpeedBuff;
   public int damageBuff;

   public int toxicityAmount;
  
   
   public void Awake(){
      type = ItemType.Consumable;
   }

   public override void UseItem(){
       Consume();
   }

   public void Consume(){
       _consumeEvent.Invoke();
   }
}
