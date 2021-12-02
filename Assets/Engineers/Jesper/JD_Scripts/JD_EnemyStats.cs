using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_EnemyStats : MonoBehaviour{

    [SerializeField] float toughness, strength, dexterity, knowledge, reflex, luck;
    public float maxHP;
    public float currentHP { get; private set; }
    public bool dealingDmg = false;


    public JD_EnemyStats(float toughness, float strength, float dexterity, float knowledge, float reflex, float luck, float currentHp,float maxHp){
        this.toughness = toughness;
        this.strength = strength;
        this.dexterity = dexterity;
        this.knowledge = knowledge;
        this.reflex = reflex;
        this.luck = luck;
        currentHP = maxHP;
        
    }

    void Start(){
        currentHP = maxHP;
    }

    void Update(){
        DealDmg();
    }

    public void DealDmg(){
        if (Input.GetKeyDown(KeyCode.E)){
            currentHP -= 10;
            FindObjectOfType<JD_UI_DamageNr>().Display();
            
        }
        Debug.Log(currentHP);
        dealingDmg = true;
    }
}
