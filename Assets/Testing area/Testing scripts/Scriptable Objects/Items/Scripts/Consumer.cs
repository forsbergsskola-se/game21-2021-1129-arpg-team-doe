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
    [SerializeField] ConsumableObject _consumableObject;
    [SerializeField] int maxToxicityLevel = 3;

    Statistics _statistics;
    Health _health;

    int toxicityLevel;

    void Awake(){
        _statistics = GetComponent<Statistics>();
        _health = GetComponent<Health>();
    }

    [ContextMenu("ConsumeItem")]
    public void ConsumeItem(){
        if (_consumableObject == null){
            return;
        }
        if (toxicityLevel < maxToxicityLevel){
           StartCoroutine(Consume());
        }
        else{
            Debug.Log("Toxicity level too high!" + toxicityLevel + "/" + maxToxicityLevel);
        }
    }

    IEnumerator Consume(){
        _health.UpdateHealth(_consumableObject.restoreHealthValue);
        _statistics.Toughness += _consumableObject.toughnessBuff;
        _statistics.Strength += _consumableObject.strengthBuff;
        _statistics.Dexterity += _consumableObject.dexterityBuff;
        _statistics.Knowledge += _consumableObject.knowledgeBuff;
        _statistics.Luck += _consumableObject.luckBuff;
        _statistics.AttackSpeed += _consumableObject.attackSpeedBuff;
        _statistics.AttackDamage += _consumableObject.damageBuff;

        toxicityLevel += _consumableObject.toxicityAmount;

        //Display buff method
        yield return new WaitForSeconds(_consumableObject.buffDuration);
        //Remove display buff method

        _statistics.Toughness -= _consumableObject.toughnessBuff;
        _statistics.Strength -= _consumableObject.strengthBuff;
        _statistics.Dexterity -= _consumableObject.dexterityBuff;
        _statistics.Knowledge -= _consumableObject.knowledgeBuff;
        _statistics.Luck -= _consumableObject.luckBuff;
        _statistics.AttackSpeed -= _consumableObject.attackSpeedBuff;
        _statistics.AttackDamage -= _consumableObject.damageBuff;

        toxicityLevel -= _consumableObject.toxicityAmount;


        //We want to Access all stat values and add our buffs/Debuffs to them
        //We want to set a timer
        //When timer is out we want to remove our added/removed values from those stats, this should not affect health
    }
}
