using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIDamageNr : MonoBehaviour{

    [SerializeField] float duration;
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    string dmgText;
    int dmg;
    bool active;
    bool takingDamage;
    Statistics _stats;
    
    Animator _animator;
    string _text;

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        _text = _textMeshProUGUI.text;
        _stats = GetComponentInParent<Statistics>();
    }

    void Update(){
        takingDamage = _stats.dealingDmg;
        CollectDmg(dmg);
        dmgText = Convert.ToString(dmg);
        
        if (takingDamage){
            Timer();
            if (duration > 0){
                _textMeshProUGUI.text= dmgText;
                _animator.Play("FloatingPoint");
            }
            else if (!takingDamage){
                _textMeshProUGUI.text = "";
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
