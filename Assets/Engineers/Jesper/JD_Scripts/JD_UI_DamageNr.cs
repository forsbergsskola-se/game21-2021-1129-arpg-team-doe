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
    
    Animator _animator;
    string _text;

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        _text = GetComponent<TextMeshProUGUI>().text;
        takingDamage = FindObjectOfType<JD_EnemyStats>().dealingDmg;
    }

    void Update(){
        CollectDmg(dmg);
        
        dmgText = Convert.ToString(dmg);
        if (takingDamage){
            Timer();
            if (duration > 0){
                _text = dmgText; //Maybe cache the TextMesh in a var, since its used twice
                _animator.Play("FloatingPoint");
            }
            else if (!takingDamage){
                _text = "";
            }
        }
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
