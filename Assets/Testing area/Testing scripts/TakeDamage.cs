using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class TakeDamage : MonoBehaviour, IDamageReceiver{

    Statistics _stats;
    Random random;
    List<IDamageNumbers> damagenumbers ;
    
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
        IDamageNumbers[] damageNumbersArray = GetComponentsInChildren<IDamageNumbers>();
        
        foreach (IDamageNumbers damageNumber in damageNumbersArray){
            if (damageNumber != null){
                damageNumber.DisplayDmg(damage, isCrit);
            }
            
        }
        
        //DamageNumbersCuller(damage,isCrit);
        Debug.Log(_currentHealth);
        Debug.Log(isCrit);
    }

    void DamageNumbersCuller(int damage, bool isCrit){
        
            

            for (int i = 0; i < damagenumbers.Count; i++){
               ;
            }
        
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
