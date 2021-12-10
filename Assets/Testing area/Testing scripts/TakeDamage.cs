using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class TakeDamage : MonoBehaviour, IDamageReceiver{

    Statistics _stats;
    Random random;
    List<IDamageNumbers> damageNumbersList;

    public bool isAlive;
    
    int _currentHealth;
    void Start(){
        _stats = GetComponent<Statistics>();
        random = new Random();
    }

    public void ReceiveDamage(int damage, bool isCrit){ //Toughness should affect this
        damage = ProcessDamage(damage);
        _stats.UpdateHealth(damage);
        GetComponent<IDestructible>()?.Destruction();
        _currentHealth = _stats.currentHP;
        GetComponentInChildren<IHealthbar>()?.SetSliderCurrentHealth(_currentHealth);
        GetComponentInChildren<ITextSpawner>()?.Spawn(damage,isCrit);
        damageNumbersList = GetComponentsInChildren<IDamageNumbers>()?.ToList();
        ActivateDamageNumbers(damage, isCrit);
        isAlive = _currentHealth <= 0;
    }

    void ActivateDamageNumbers(int damage, bool isCrit){
        foreach (IDamageNumbers damageNumber in damageNumbersList){
            if (damageNumber != null){
                damageNumber.DisplayDmg(damage, isCrit);
            }
            else{
                damageNumbersList.Remove(damageNumber);
            }
        }
    }

    bool DodgeSuccessful(){
        return random.NextDouble() < _stats.DodgeChance;
    }

    int ProcessDamage(int dmg){
        if (DodgeSuccessful()){
            dmg = 0;
        }
        Debug.Log(transform.name + " receives " + dmg + " Damage");
        return dmg;
    }
}
