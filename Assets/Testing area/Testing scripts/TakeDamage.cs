using System;
using UnityEngine;
using Random = System.Random;

public class TakeDamage : MonoBehaviour, IDamageReceiver{

    Statistics _stats;
    Random random;
    
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
        GetComponent<IHealthbar>()?.SetSliderCurrentHealth(_currentHealth);
        GetComponent<IDamageNumbers>()?.DisplayDmg(damage, isCrit);
        Debug.Log(_currentHealth);
        Debug.Log(isCrit);
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
