using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IConsumable{
    public void Consume(){
    }
}
public class Consumer : MonoBehaviour, IConsumable
{
    
    //How it should work
    // * If right clicking on consumable object in inventory, set that object as _consumableObject
    // * Then call that consumable objects use function which holds the consume method.
    
    [SerializeField] ConsumableObject _consumableObject;
    [SerializeField] int maxToxicityLevel = 3;

    public Statistics _statistics;
    public Health _health;

    int toxicityLevel;

    void Awake(){
        _statistics = GetComponent<Statistics>();
        _health = GetComponent<Health>();
    }


    public void Consume(){
        if (_consumableObject is StatBuffObject){
            ConsumeItem((StatBuffObject)_consumableObject);
        }

        if (_consumableObject is HealingObject){
            ConsumeItem((HealingObject)_consumableObject);
        }
        
    }

    [ContextMenu("ConsumeItem")]
    public void ConsumeItem(HealingObject consumedItem){
        if (_consumableObject == null){
            return;
        }

        if (toxicityLevel < maxToxicityLevel){
            StartCoroutine(AddHealthCoroutine(consumedItem));
        }
        else{
            Debug.Log("Toxicity level too high!" + toxicityLevel + "/" + maxToxicityLevel);
        }
    }

    public void ConsumeItem(StatBuffObject consumedItem){
        if (_consumableObject == null){
            return;
        }
        if (toxicityLevel < maxToxicityLevel){
            StartCoroutine(AddStatsCoroutine(consumedItem));
        }
        
        else{
            Debug.Log("Toxicity level too high!" + toxicityLevel + "/" + maxToxicityLevel);
        }
    }

    IEnumerator AddStatsCoroutine(StatBuffObject consumedItem){
        _statistics.AddStats(consumedItem.toughnessBuff,consumedItem.strengthBuff,consumedItem.dexterityBuff,consumedItem.knowledgeBuff,consumedItem.reflexBuff,consumedItem.luckBuff,consumedItem.interactRangeBuff,consumedItem.attackRangeBuff,consumedItem.attackSpeedBuff,consumedItem.damageBuff);
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.buffDuration);
        _statistics.AddStats(-consumedItem.toughnessBuff,-consumedItem.strengthBuff,-consumedItem.dexterityBuff,-consumedItem.knowledgeBuff,-consumedItem.reflexBuff,-consumedItem.luckBuff,-consumedItem.interactRangeBuff,-consumedItem.attackRangeBuff,-consumedItem.attackSpeedBuff,-consumedItem.damageBuff);
        toxicityLevel -= consumedItem.toxicityAmount;
    }
    
    IEnumerator AddHealthCoroutine(HealingObject consumedItem){
        _health.UpdateHealth(consumedItem.restoreHealthValue);
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.toxicityDuration);
        toxicityLevel -= consumedItem.toxicityAmount;
    }



    // IEnumerator Consume(){
    //     _health.UpdateHealth(_consumableObject.restoreHealthValue);
    //     _statistics.Toughness += _consumableObject.toughnessBuff;
    //     _statistics.Strength += _consumableObject.strengthBuff;
    //     _statistics.Dexterity += _consumableObject.dexterityBuff;
    //     _statistics.Knowledge += _consumableObject.knowledgeBuff;
    //     _statistics.Luck += _consumableObject.luckBuff;
    //     _statistics.AttackSpeed += _consumableObject.attackSpeedBuff;
    //     _statistics.AttackDamage += _consumableObject.damageBuff;
    //
    //     toxicityLevel += _consumableObject.toxicityAmount;
    //
    //     //Display buff method
    //     yield return new WaitForSeconds(_consumableObject.buffDuration);
    //     //Remove display buff method
    //
    //     _statistics.Toughness -= _consumableObject.toughnessBuff;
    //     _statistics.Strength -= _consumableObject.strengthBuff;
    //     _statistics.Dexterity -= _consumableObject.dexterityBuff;
    //     _statistics.Knowledge -= _consumableObject.knowledgeBuff;
    //     _statistics.Luck -= _consumableObject.luckBuff;
    //     _statistics.AttackSpeed -= _consumableObject.attackSpeedBuff;
    //     _statistics.AttackDamage -= _consumableObject.damageBuff;
    //
    //     toxicityLevel -= _consumableObject.toxicityAmount;
    //
    //
    //     //We want to Access all stat values and add our buffs/Debuffs to them
    //     //We want to set a timer
    //     //When timer is out we want to remove our added/removed values from those stats, this should not affect health
    // }
}
