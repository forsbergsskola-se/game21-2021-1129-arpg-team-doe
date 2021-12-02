using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class JD_UI_DamageNr : MonoBehaviour{

    string dmgText;
    int dmg;
    [SerializeField] float duration;
    bool active;
    Animator anim;

    void Start(){
        anim = gameObject.GetComponent<Animator>();
    }

    void Update(){
        CollectDmg(dmg);
        
        dmgText = Convert.ToString(dmg);
        if (FindObjectOfType<JD_EnemyStats>().dealingDmg){
            Timer();
            if (duration > 0){
                GetComponent<TextMeshProUGUI>().text = dmgText;
                anim.Play("FloatingPoint");
            }
            else if (!FindObjectOfType<JD_EnemyStats>().dealingDmg){
                GetComponent<TextMeshProUGUI>().text = "";
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
        FindObjectOfType<JD_EnemyStats>().dealingDmg = false;
        
    }

    int CollectDmg(int damage){ 
        // Put real logic here

        damage = 10; 
        return  dmg = damage;
    }
}
