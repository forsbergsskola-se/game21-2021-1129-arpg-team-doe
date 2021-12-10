using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class TakeDamage : MonoBehaviour, IDamageReceiver{

    Statistics _stats;
    Random random;
    List<IDamageNumbers> damagenumbers ;

    List<IDamageNumbers> damageNumbersList;
    
    int _currentHealth;
    bool _dodged;
    

    void Start(){
        _stats = GetComponent<Statistics>();
        random = new Random();
    }

    void OnMouseEnter(){
        ReceiveDamage(10,false);
    }

    public void ReceiveDamage(int damage, bool isCrit){ //Toughness should affect this
        int currentDamage = DamageCalc(damage);
        _stats.UpdateHealth(currentDamage);
        GetComponent<IDestructible>()?.Destruction(damage);
        _currentHealth = _stats.currentHP;
        GetComponent<IHealthbar>()?.SetSliderCurrentHealth(_currentHealth);
        GetComponentInChildren<ITextSpawner>()?.Spawn(damage,isCrit);
        damageNumbersList = GetComponentsInChildren<IDamageNumbers>()?.ToList();
        
        foreach (IDamageNumbers damageNumber in damageNumbersList){
            if (damageNumber != null){
                damageNumber.DisplayDmg(damage, isCrit);
            }
            else{
                damageNumbersList.Remove(damageNumber);
            }
        }
        
        //DamageNumbersCuller(damage,isCrit);
        Debug.Log(this.name + " health: " + _currentHealth);
        //Debug.Log(isCrit);
    }

    bool DodgeDamage(){
        var dodgeChance = _stats.Reflex;//This calculation should be in statistics?

        if (random.NextDouble() < dodgeChance){
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
