using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour{

    [SerializeField] float toughness, strength, dexterity, knowledge, reflex, luck;
    public int maxHP;
    public int currentHP { get; private set; } 
    public bool dealingDmg; //used for debug


    public Statistics(float toughness, float strength, float dexterity, float knowledge, float reflex, float luck,int currentHp,int maxHp){
        this.toughness = toughness;
        this.strength = strength;
        this.dexterity = dexterity;
        this.knowledge = knowledge;
        this.reflex = reflex;
        this.luck = luck;
        this.maxHP = maxHp;
    }

    void Start(){
        currentHP = maxHP; 
    }

    void Update(){
        DealDmg(); 
    }

    public void DealDmg(){ //used for debug
        if (Input.GetKeyDown(KeyCode.E)){
            dealingDmg = true;
            currentHP -= 10;
            FindObjectOfType<UIDamageNr>().Display();
            
        }
    }
}
