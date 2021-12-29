using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
    [SerializeField] internal List<DamageType> vulnerabilities;
    [Tooltip("The higher the value, the more damage is taken")][Range(1f,3f)][SerializeField] 
    internal float vulnerabilityDamageModifier = 2f;
    [SerializeField] internal List<DamageType> resistances;
    [Tooltip("The higher the value, the more damage is taken")][Range(-1f,1f)][SerializeField] 
    internal float resistanceDamageModifier = 0.5f;
    int damage;
    bool isRanged;

    public float Toughness{
        get => toughness;
         set => toughness = value;
    }


    public float Strength{
        get => strength; 
        set => strength = value;
    }

    public float Dexterity{
        get => dexterity;
        set => dexterity = value;
    }

    public float Knowledge{
        get => knowledge;
        set => knowledge = value;
    }

    public float Reflex{
        //0-100 dodge chance
        get => reflex;
        set => reflex = value;
    }

    public float Luck{
        get => luck;
        set => luck = value;
    }

    public float InteractRange{
        get { return interactRange; }
        set { interactRange = value; }
    }

    public float AttackRange{
        get { return attackRange; }
        set { attackRange = value; }
    }

    public float AttackSpeed{
        get{
            return CalculateAttackSpeed();
            // 1 dexterity -> 0.5 percent change, called in Fighter
        }
        set{
            attackSpeed = value;
        }
    }

    public int AttackDamage{
        get{
            return CalculateWeaponDamage();
            // called in Fighter
        }
        set{
            damage = value;
        }
    }

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

    public void AddStats(float toughnessBuff, float strengthBuff, float dexterityBuff, float knowledgeBuff, float reflexBuff, float luckBuff,
        float interactRangeBuff, float attackRangeBuff, float attackSpeedBuff,int damageBuff){
        Toughness += toughnessBuff;
        Strength += strengthBuff;
        Dexterity += dexterityBuff;
        Knowledge += knowledgeBuff;
        Reflex += reflexBuff;
        Luck += luckBuff;
        InteractRange += interactRangeBuff;
        AttackRange += attackRangeBuff;
        AttackSpeed += attackSpeedBuff;
        AttackDamage += damageBuff;
    }

    //Takes in temp values and then starts a coroutine to add them then after a duration it removes them.
    // public void AddStatsTemp(int buffDuration,float toughnessBuff, float strengthBuff, 
    //     float dexterityBuff, float knowledgeBuff, float reflexBuff,
    //     float luckBuff, float interactRangeBuff, float attackRangeBuff, 
    //     float attackSpeedBuff,int damageBuff){
    //     
    //     StartCoroutine(AddStatsCoroutine(buffDuration,toughnessBuff,strengthBuff,dexterityBuff,knowledgeBuff,reflexBuff,luckBuff,interactRangeBuff,attackRangeBuff,attackSpeedBuff,damageBuff));
    // }

    // IEnumerator AddStatsCoroutine(int buffDuration,float toughnessBuff, float strengthBuff, 
    //     float dexterityBuff, float knowledgeBuff, float reflexBuff, 
    //     float luckBuff, float interactRangeBuff, float attackRangeBuff, 
    //     float attackSpeedBuff,int damageBuff){
    //     
    //     
    //     AddStats(toughnessBuff,strengthBuff,dexterityBuff,knowledgeBuff,reflexBuff,luckBuff,interactRangeBuff,attackRangeBuff,attackSpeedBuff,damageBuff);
    //     yield return new WaitForSeconds(buffDuration);
    //     AddStats(-toughnessBuff,-strengthBuff,-dexterityBuff,-knowledgeBuff,-reflexBuff,-luckBuff,-interactRangeBuff,-attackRangeBuff,-attackSpeedBuff,-damageBuff);
    //
    // }

    public void AddToughness(int amount){
       Toughness += amount;
    }
    public void AddStrength(int amount){
        Strength += amount;
    }
    public void AddDexterity(int amount){
        Dexterity += amount;
    }
    public void AddKnowledge(int amount){
        Knowledge += amount;
    }
    public void AddReflex(int amount){
        Reflex += amount;
    }
    public void AddLuck(int amount){
        Luck += amount;
    }
    

#if UNITY_EDITOR
    void OnDrawGizmosSelected(){
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up,AttackRange);
        Handles.color = Color.gray;
        Handles.DrawWireDisc(transform.position,transform.up,interactRange);
        
    }
#endif
}
