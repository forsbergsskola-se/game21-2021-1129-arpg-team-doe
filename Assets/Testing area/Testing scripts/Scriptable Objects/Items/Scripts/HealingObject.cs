using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Healing Object", menuName = "Inventory System/Items/Consumable/Healing Object")]
public class HealingObject : ConsumableObject
{
    public int restoreHealthValue;
    public int toxicityDuration;


    public override void Consume(){
        base.Consume();
      //  _consumer.ConsumeItem(this);
    }
}
