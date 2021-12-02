using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_EnemyStats : MonoBehaviour{ //Revamped into Stats and it works for both player and enemies?

    [SerializeField] float toughness, strength, dexterity, knowledge, reflex, luck;
    public float maxHP; //Int! floats are to annoying when it comes to health
    public float currentHP { get; private set; } //Int
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
        currentHP = maxHP; //Redundant because of constructor
    }

    void Update(){
        DealDmg(); //Used for debugging? Probably best to keep the two things apart, damage should be dealt with somewhere else
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
