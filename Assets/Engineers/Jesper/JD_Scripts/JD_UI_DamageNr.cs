using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using TMPro;
using UnityEngine;

public class JD_UI_DamageNr : MonoBehaviour{

    string dmgText;
    int dmg;
    [SerializeField] float duration;
    bool active;

    void Update(){
        CollectDmg(dmg);

        dmgText = Convert.ToString(dmg);
        if (FindObjectOfType<JD_EnemyStats>().dealingDmg){

            Timer();
            FloatAway();
            if (duration > 0){
                GetComponent<TextMeshProUGUI>().text = dmgText;
                Debug.Log("Damage");
            }
            else if (!FindObjectOfType<JD_EnemyStats>().dealingDmg){
                GetComponent<TextMeshProUGUI>().text = "";
                Debug.Log("No Damage");
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

    void FloatAway(){
        this.transform.position = Vector3.Lerp(this.transform.position, (transform.position + Vector3.forward), 0.1f);

    }
}
