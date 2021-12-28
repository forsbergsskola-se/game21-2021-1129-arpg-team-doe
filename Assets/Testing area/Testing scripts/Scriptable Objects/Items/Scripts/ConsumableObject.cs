using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public abstract class ConsumableObject : ItemObject, IConsumable{
  //  [SerializeField] protected GameEvent _consumeEvent;
   // [SerializeField] protected Consumer _consumer;
    public FMODUnity.EventReference fmodEvent;
   

   public int toxicityAmount;


   public void Awake() {
       this.GameObject();
       type = ItemType.Consumable;
   }

   public override void UseItem(){ //When used in inventory
       Consume();
   }

   public virtual void Consume()
   {
      // _consumeEvent.Invoke();
       
   }
   
   
}
