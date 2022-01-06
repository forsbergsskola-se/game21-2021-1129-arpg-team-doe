using System;
using System.Collections;
using System.Collections.Generic;
using CustomLogs;
using Unity.VisualScripting;
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
    
    [HideInInspector] public ConsumableObject _consumableObject;
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

        if (_consumableObject is TeleportPotionSO)
        {
            ConsumeItem((TeleportPotionSO)_consumableObject);
        }
            
        
    }

    [ContextMenu("ConsumeItem")]
     void ConsumeItem(HealingObject consumedItem){
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

    void ConsumeItem(StatBuffObject consumedItem){
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
    
     void ConsumeItem(TeleportPotionSO consumedItem){
        if (_consumableObject == null){
            return;
        }
        if (toxicityLevel < maxToxicityLevel){
            StartCoroutine(TeleportToTargetCoroutine(consumedItem));
        }
        
        else{
            Debug.Log("Toxicity level too high!" + toxicityLevel + "/" + maxToxicityLevel);
        }
    }

    IEnumerator AddStatsCoroutine(StatBuffObject consumedItem){
        this.Log("Consumed Stat Buff");
        _statistics.AddStats(consumedItem.toughnessBuff,consumedItem.strengthBuff,consumedItem.dexterityBuff,consumedItem.knowledgeBuff,consumedItem.reflexBuff,consumedItem.luckBuff,consumedItem.interactRangeBuff,consumedItem.attackRangeBuff,consumedItem.attackSpeedBuff,consumedItem.damageBuff);
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.buffDuration);
        _statistics.AddStats(-consumedItem.toughnessBuff,-consumedItem.strengthBuff,-consumedItem.dexterityBuff,-consumedItem.knowledgeBuff,-consumedItem.reflexBuff,-consumedItem.luckBuff,-consumedItem.interactRangeBuff,-consumedItem.attackRangeBuff,-consumedItem.attackSpeedBuff,-consumedItem.damageBuff);
        toxicityLevel -= consumedItem.toxicityAmount;
        this.Log(toxicityLevel);
    }
    
    IEnumerator AddHealthCoroutine(HealingObject consumedItem){
        this.Log("Consumed Health Potion for" + consumedItem.restoreHealthValue);
        _health.UpdateHealth(consumedItem.restoreHealthValue);
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.toxicityDuration);
        toxicityLevel -= consumedItem.toxicityAmount;
        this.Log(toxicityLevel);
    }
    
    IEnumerator TeleportToTargetCoroutine(TeleportPotionSO consumedItem){
        this.Log("Starts Teleporting");
        yield return new WaitForSeconds(consumedItem.durationUntilTeleport);
        gameObject.transform.position = consumedItem.targetTeleportLocation;
        toxicityLevel += consumedItem.toxicityAmount;
        yield return new WaitForSeconds(consumedItem.toxicityDuration);
        toxicityLevel -= consumedItem.toxicityAmount;
        this.Log(toxicityLevel);
    }
}