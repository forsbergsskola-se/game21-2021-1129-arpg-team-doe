using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Stat Buff Object", menuName = "Inventory System/Items/Consumable/Stat Buff Object")]
public class StatBuffObject : ConsumableObject
{
    [Header("Attribute Buffs")]
    [Min(0f)][Tooltip	("Duration in Seconds")] public int buffDuration;
    public float toughnessBuff;
    public float strengthBuff;
    public float dexterityBuff;
    public float knowledgeBuff;
    public float reflexBuff;
    public float luckBuff;
    public float interactRangeBuff;
    public float attackRangeBuff;
    [Tooltip	("Amount of attacks per second Buff")] public float attackSpeedBuff;
    public int damageBuff;

    public override void Consume(){
        base.Consume();
      //  _consumer.ConsumeItem(this);
        // ConsumeWithDuration();
        

    }

    // public void ConsumeWithDuration(){
    //     _consumer._statistics.AddStatsTemp(buffDuration,toughnessBuff,strengthBuff,dexterityBuff,knowledgeBuff,reflexBuff,luckBuff,interactRangeBuff,attackRangeBuff,attackSpeedBuff,damageBuff);
    // }
}
