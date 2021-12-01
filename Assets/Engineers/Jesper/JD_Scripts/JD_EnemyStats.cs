using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_EnemyStats : MonoBehaviour{

    [SerializeField] float constitution, strength, dexterity, wisdom, charisma, luck;
    public float maxHP;
    public float currentHP { get; private set; }
    public bool dealingDmg = false;

    public JD_EnemyStats(float constitution, float strength, float dexterity, float wisdom, float charisma, float luck, float currentHp, float maxHp){
        this.constitution = constitution;
        this.strength = strength;
        this.dexterity = dexterity;
        this.wisdom = wisdom;
        this.charisma = charisma;
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
