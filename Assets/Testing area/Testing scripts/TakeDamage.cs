using UnityEngine;
using Random = System.Random;

public class TakeDamage : MonoBehaviour, IDamageReceiver{

    Statistics _stats;
    Random random;
    
    int _currentHealth;
    bool _dodged;

    void Start(){
        _stats = GetComponent<Statistics>();
        _currentHealth = _stats.currentHP;
        random = new Random();
    }

    public void ReceiveDamage(int damage){ //Toughness should affect this
        _currentHealth -= DamageCalc(damage);
        GetComponent<IDestructible>()?.Destruction(damage);
        GetComponent<IHealthbar>()?.SetSliderCurrentHealth(_currentHealth);
    }

    bool DodgeDamage(){
        var dodgeChance = _stats.Reflex;

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
        Debug.Log("Receiving " + dmg + " Damage");
        return dmg;
    }
}
