using System;
using System.Collections.Generic;
using UnityEngine;


public class Statistics : MonoBehaviour{

    [SerializeField]
    [Min(0f)]
    float toughness, strength, dexterity, knowledge, reflex, luck, interactRange, attackRange, attackSpeed, critChance;
    // movement is increased by reflex

    [SerializeField] int weaponDamage = 10; // for debug
    [SerializeField] int defaultDamage = 5;
    [SerializeField] internal float lowImpactLevelMultiplier = 0.5f;
    [SerializeField] internal float highImpactLevelMultiplier = 1f;
    [SerializeField] List<DamageType> vulnerabilities;
    int damage;
    bool isRanged;

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

    public float Reflex{
        //0-100 dodge chance
        get => reflex;
        private set => reflex = value;
    }

    public float Luck{
        get => luck;
        private set => luck = value;
    }

    public float InteractRange{
        get { return interactRange; }
        private set { interactRange = value; }
    }

    public float AttackRange{
        get { return attackRange; }
        private set { attackRange = value; }
    }

    public float AttackSpeed => CalculateAttackSpeed(); // 1 dexterity -> 0.5 percent change, called in Fighter
    public int AttackDamage => CalculateWeaponDamage(); // called in Fighter
    public float CritChance => CalculateCritChance(); // called in Fighter
    public float DodgeChance => CalculateDodgeChance();  // called in TakeDamage

    public float StatManipulation(int baseValue, float attribute, float levelMultiplier){
        return (baseValue * (1 + attribute * levelMultiplier));
    }

    public float StatManipulation(float baseValue, float attribute, float levelMultiplier){
        return (baseValue * (1 + attribute * levelMultiplier));
    }

    public Statistics(float toughness, float strength, float dexterity, float knowledge, float reflex, float luck,
        float interactRange, float attackRange, float attackSpeed) {
        this.toughness = toughness;
        this.strength = strength;
        this.dexterity = dexterity;
        this.knowledge = knowledge;
        this.reflex = reflex;
        this.luck = luck;
        this.interactRange = interactRange;
        this.attackRange = attackRange;
        this.attackSpeed = attackSpeed;
        this.critChance = critChance;
        damage = defaultDamage;
    }

    float CalculateAttackSpeed(){
        return StatManipulation(attackSpeed, dexterity, lowImpactLevelMultiplier);
    }
    float CalculateCritChance(){
        return Luck * lowImpactLevelMultiplier;
    }
    float CalculateDodgeChance(){
        return reflex * highImpactLevelMultiplier;
    }

    // used for debug
    int CalculateWeaponDamage(){
        float damageMultiplier;
        if (isRanged){
            damageMultiplier = dexterity * highImpactLevelMultiplier + 1;
        }
        else{
            damageMultiplier = strength * highImpactLevelMultiplier + 1;
        }
        return damage = (int) (weaponDamage * damageMultiplier);
    }
}
