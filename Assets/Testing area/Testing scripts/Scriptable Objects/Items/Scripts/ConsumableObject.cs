using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


   public void Awake() {
       this.GameObject();
       type = ItemType.Consumable;
   }

   public virtual void UseItem(){
       Consume();
   }

   public virtual void Consume(){
       _consumeEvent.Invoke();
   }
}
