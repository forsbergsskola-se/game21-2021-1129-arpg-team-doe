using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JD_UI_DamageNr : MonoBehaviour{

    [SerializeField] float duration;
    
    string dmgText;
    int dmg;
    bool active;
    bool takingDamage;
    JD_EnemyStats _enemyStats;
    
    Animator _animator;
    string _text;

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        _text = GetComponent<TextMeshProUGUI>().text;
        _enemyStats = FindObjectOfType<JD_EnemyStats>();
    }

    void Update(){
        takingDamage = _enemyStats.dealingDmg;
        CollectDmg(dmg);
        dmgText = Convert.ToString(dmg);
        
        if (takingDamage){
            Timer();
            if (duration > 0){
                GetComponent<TextMeshProUGUI>().text= dmgText; //Maybe cache the TextMesh in a var, since its used twice
                _animator.Play("FloatingPoint");
            }
            else if (!takingDamage){
                GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        takingDamage = false;
    }


    public void Display(){
        active = true;
        duration = 1;
    }
    void Timer(){
        if (active){
            duration -= Time.deltaTime;
            if (duration <= 0f){
                End();
            }
        }
    }

    void End(){
        active = false;
        takingDamage = false;
    }

    int CollectDmg(int damage){ //used for Debug
        // Put real logic here
        damage = 10; 
        return  dmg = damage;
    }
}
