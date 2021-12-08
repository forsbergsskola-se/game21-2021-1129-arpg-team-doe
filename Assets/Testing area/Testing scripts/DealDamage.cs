using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public interface IDamageReceiver{
    void ReceiveDamage(int damage);
}

public class DealDamage : MonoBehaviour{
    [SerializeField] float critDamageMultiplier = 1.5f;

    Statistics _statistics;
    Random _random;

    float _attackRange;
    float _attackSpeed;
    float _critChance;
    float _distance;
    bool isRanged;

    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        _attackSpeed = _statistics.AttackSpeed;
        _random = new Random();
        InvokeRepeating(nameof(Attack),0, 1f/ _attackSpeed);
    }

    public void Attack(int damage, GameObject target){
        if (_random.NextDouble() < _critChance){
            damage = Mathf.RoundToInt(damage * critDamageMultiplier);
        }

        
        target.GetComponent<IDamageReceiver>()?.ReceiveDamage(damage); // check!!!
        Debug.Log("Dealing " + damage + " Damage");
        //return damage; //Is this needed?
    }

    // int DoMeleeDamage(int damage, GameObject target){
    //     return DamageManipulation(damage, target, _statistics.Strength);
    // }
    //
    // int DoRangedDamage(int damage, GameObject target){
    //     return DamageManipulation(damage, target, _statistics.Dexterity);
    // }

    int DamageManipulation(int damage, GameObject target, float modifier ){
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance <= _attackRange){
            //Attack
            damage = Mathf.RoundToInt((modifier * 0.01f + 1) * damage);
        }
        return damage;
    }
}
