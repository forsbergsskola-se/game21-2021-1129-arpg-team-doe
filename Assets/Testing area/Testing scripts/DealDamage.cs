using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver{
    void ReceiveDamage(int damage);
}

public class DealDamage : MonoBehaviour{

    Statistics _statistics;

    float _attackRange;
    float _attackSpeed;
    float _distance;
    bool isRanged;

    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        _attackSpeed = _statistics.AttackSpeed;
        InvokeRepeating(nameof(Attack),0, 1f/ _attackSpeed);
    }

    public int Attack(int damage, GameObject target){
        if (isRanged){
            damage = DoRangedDamage(damage, target);
        }
        else{
            damage = DoMeleeDamage(damage, target);
        }
        target.GetComponent<IDamageReceiver>()?.ReceiveDamage(damage); // check!!!
        Debug.Log("Dealing " + damage + " Damage");
        return damage;
    }

    int DoMeleeDamage(int damage, GameObject target){
        return DamageManipulation(damage, target, _statistics.Strength);
    }

    int DoRangedDamage(int damage, GameObject target){
        return DamageManipulation(damage, target, _statistics.Dexterity);
    }

    int DamageManipulation(int damage, GameObject target, float modifier ){
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance <= _attackRange){
            //Attack
            damage = Mathf.RoundToInt((modifier * 0.01f + 1) * damage);
        }
        return damage;
    }
}
