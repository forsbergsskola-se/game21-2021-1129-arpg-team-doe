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
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] GameObject Txt;
    
    string dmgText;
    int dmg;
    bool active;
    bool takingDamage;
    JD_EnemyStats _enemyStats;
    
    Animator _animator;
    string _text;

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        _text = _textMeshProUGUI.text;
        _enemyStats = FindObjectOfType<JD_EnemyStats>();
    }

    void Update(){
        var textprefab = Txt;
        takingDamage = _enemyStats.dealingDmg;
        CollectDmg(dmg);
        dmgText = Convert.ToString(dmg);
        
        if (takingDamage){
            Timer();
            Instantiate(textprefab);
            if (duration > 0){
                _textMeshProUGUI.text= dmgText;
                _animator.Play("FloatingPoint");
            }
            else if (!takingDamage){
                _textMeshProUGUI.text = "";
            }
        }
        takingDamage = false;
        Destroy(textprefab);
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
