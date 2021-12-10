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
    bool _dodged;
    void Start(){
        _stats = GetComponent<Statistics>();
        random = new Random();
    }

    public void ReceiveDamage(int damage, bool isCrit){ //Toughness should affect this
        int currentDamage = DamageCalc(damage);
        _stats.UpdateHealth(currentDamage);
        GetComponent<IDestructible>()?.Destruction(damage);
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

    bool DodgeDamage(){
        if (random.NextDouble() < _stats.DodgeChance){
            _dodged = true;
        }
        else{
            _dodged = false;
        }
        return _dodged;
    }

    int DamageCalc(int dmg){
        if (DodgeDamage()){
            dmg = 0;
        }
        Debug.Log(transform.name + " receives " + dmg + " Damage");
        return dmg;
    }
}
