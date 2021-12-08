using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour{
    
    [SerializeField] float toughness, strength, dexterity, knowledge, reflex, luck, interactRange, attackRange, attackSpeed;

    public float Toughness{
        get => toughness;
        private set => toughness = value;
    }
    public float Strength{
        get => strength;
        private set => strength = value;
    }
    public float Dexterity{
        get => dexterity;
        private set => dexterity = value;
    }
    public float Knowledge{
        get => knowledge;
        private set => knowledge = value;
    }
    public float Reflex{ //0-100 dodge chance
        get => reflex;
        private set => reflex = value;
    }
    public float Luck{
        get => luck;
        private set => luck = value;
    }

    public int maxHP;
    public int currentHP { get; private set; }

    public float InteractRange{
        get{ return interactRange; }
        //private set{ interactRange = value; }
    }
    public float AttackRange{
        get{ return attackRange; }
        private set{ attackRange = value; }
    }
    public float AttackSpeed{
        get{ return attackSpeed; }
        private set{ attackSpeed = value * (this.dexterity * 0.01f + 1); }
    }

    public bool IsAlive => currentHP > 0;

        public bool dealingDmg; //used for debug

    public Statistics(float toughness, float strength, float dexterity, float knowledge, float reflex, float luck, float interactRange, float attackRange, float attackSpeed, int maxHp, bool dealingDmg){
        this.toughness = toughness;
        this.strength = strength;
        this.dexterity = dexterity;
        this.knowledge = knowledge;
        this.reflex = reflex; 
        this.luck = luck;
        this.interactRange = interactRange;
        this.attackRange = attackRange;
        this.attackSpeed = attackSpeed;
        maxHP = maxHp;
        this.dealingDmg = dealingDmg;
    }

    void Start(){
        currentHP = maxHP; 
    }

    void Update(){
        DealDmg(); 
    }

    float AttacksPerSecond(){
        return attackSpeed * (this.dexterity * 0.01f + 1);
    }

    public void DealDmg(){ //used for debug
        if (Input.GetKeyDown(KeyCode.E)){
            dealingDmg = true;
            currentHP -= 10;
            FindObjectOfType<UIDamageNr>().Display();
            
        }
    }
}
