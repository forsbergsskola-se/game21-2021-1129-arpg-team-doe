using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Statistics : MonoBehaviour{
    [SerializeField]
    [Min(0f)]
    float toughness, strength, dexterity, knowledge, reflex, luck, interactRange, attackRange, attackSpeed, moveSpeed, critChance, consumableEffectIncrease, experienceIncrease;
    // movement is increased by reflex

    [SerializeField] int weaponDamage = 10; // for debug
    [SerializeField] internal List<DamageType> vulnerabilities;
    [Tooltip("The higher the value, the more damage is taken")][Range(1f,3f)][SerializeField] 
    internal float vulnerabilityDamageModifier = 2f;
    [SerializeField] internal List<DamageType> resistances;

    [Tooltip("The higher the value, the more damage is taken")] [Range(-1f, 1f)] [SerializeField]

    internal float damageLevelMultiplier = 0.01f;
    internal float movementSpeedMultiplier = 0.03f;
    internal float dodgeImpactLevelMultiplier = 0.005f;
    internal float lowImpactLevelMultiplier = 0.05f;
    internal float lowImpactLuckMultiplier = 0.01f;
    internal float toughnessDamageReductionMultiplier = 0.1f;
    internal float highImpactLevelMultiplier = 0.1f;
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
        private set{
            attackSpeed = value;
        }
    }

    public int AttackDamage{
        get{
            return CalculateWeaponDamage();
            // called in Fighter
        }
        private set{
            damage = value;
        }
    }

    public float MoveSpeedIncrease => CalculateMoveSpeed();
    public float ExperienceIncrease => CalculateKnowledgeChance(experienceIncrease); //TODO:needs to impact % of xp gain, but where do we call it?
    public float ConsumableEffectIncrease => CalculateKnowledgeChance(consumableEffectIncrease); // called in Consumer
    public float CritChance => CalculateCritChance(); // called in Fighter
    public float DodgeChance => CalculateDodgeChance();  // called in Health
    
    public float LuckChance => CalculateLuckChance(); //called in LootTable

    public float ToughnessDamageReduction => CalculateToughnessDamageReduction();

    public float StatManipulation(int baseValue, float attribute, float levelMultiplier){
        return (baseValue * (1 + attribute * levelMultiplier));
    }

    public float StatManipulation(float baseValue, float attribute, float levelMultiplier){
        return (baseValue * (1 + attribute * levelMultiplier));
    }

    float CalculateAttackSpeed(){
        return StatManipulation(attackSpeed, dexterity, lowImpactLevelMultiplier);
    }

    float CalculateMoveSpeed(){
        return dexterity * movementSpeedMultiplier;
    }
    float CalculateCritChance(){
        return Luck * lowImpactLuckMultiplier;
    }
    float CalculateDodgeChance(){
        return reflex * dodgeImpactLevelMultiplier;
    }

    float CalculateToughnessDamageReduction()
    {
        return Toughness * toughnessDamageReductionMultiplier;
    }
    
    float CalculateLuckChance(){
        return Luck * lowImpactLuckMultiplier;
    }

    float CalculateKnowledgeChance(float input){
        return StatManipulation(input, knowledge, lowImpactLevelMultiplier);
    }

    // used for debug
    int CalculateWeaponDamage(){
        float damageMultiplier;
        if (isRanged){
            damageMultiplier = dexterity * damageLevelMultiplier + 1;
        }
        else{
            damageMultiplier = strength * damageLevelMultiplier + 1;
        }
        return damage = (int) (weaponDamage * damageMultiplier);
    }

    public void AddStats(float toughnessBuff, float strengthBuff, float dexterityBuff, float knowledgeBuff, float reflexBuff, float luckBuff,
        float interactRangeBuff, float attackRangeBuff, float attackSpeedBuff,int damageBuff){
        Toughness += toughnessBuff * consumableEffectIncrease;
        Strength += strengthBuff * consumableEffectIncrease;
        Dexterity += dexterityBuff * consumableEffectIncrease;
        Knowledge += knowledgeBuff * consumableEffectIncrease;
        Reflex += reflexBuff * consumableEffectIncrease;
        Luck += luckBuff * consumableEffectIncrease;
        InteractRange += interactRangeBuff * consumableEffectIncrease;
        AttackRange += attackRangeBuff * consumableEffectIncrease;
        AttackSpeed += attackSpeedBuff * consumableEffectIncrease;
        AttackDamage += (int)(damageBuff * consumableEffectIncrease);
    }

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
