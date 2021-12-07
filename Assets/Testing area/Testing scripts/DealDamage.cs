using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour{

    Statistics _statistics;

    float _attackRange;
    float _distance;


    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        
    }

    public int DoMeleeDamage(int damage, Transform target){ 
        return DamageManipulation(damage, target, _statistics.Strength);
    }
    
    public int DoRangedDamage(int damage, Transform target){
        return DamageManipulation(damage, target, _statistics.Dexterity);
    }

    void AttackSpeed(){
       // StartCoroutine()
    }

    // IEnumerator Wait(){
  //  //    // yield return new WaitForSeconds(_statistics.Dexterity * -0.01f + 1);
    // }

    int DamageManipulation(int damage, Transform target, float modifier ){
        _distance = Vector3.Distance(transform.position, target.position);
        if (_distance <= _attackRange){
            //Attack
            damage = Mathf.RoundToInt((modifier * 0.01f + 1) * damage);
        }

        return damage;
    }
}
