using System;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Statistics : MonoBehaviour{
    [SerializeField]
    [Min(0f)]
    float toughness, strength, dexterity, knowledge, reflex, luck, interactRange, attackRange, attackSpeed, moveSpeed, critChance, consumableEffectIncrease, experienceIncrease;
    // movement is increased by reflex

    [SerializeField] int weaponDamage = 10; 
    [SerializeField] internal List<DamageType> vulnerabilities;
    [Tooltip("The higher the value, the more damage is taken")][Range(1f,3f)][SerializeField] 
    internal float vulnerabilityDamageModifier = 2f;
    [SerializeField] internal List<DamageType> resistances;

    [Tooltip("The higher the value, the more damage is taken")] [Range(-1f, 1f)] [SerializeField]
    internal float damageLevelMultiplier = 0.01f;
    
    internal float movementSpeedMultiplier = 0.03f;
    internal float dodgeImpactLevelMultiplier = 0.005f;
    internal float lowImpactLevelMultiplier = 0.05f;
    internal float lowImpactMultiplier = 0.01f;
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

    public float InteractRange => interactRange;
    public float AttackRange => attackRange;
    public float AttackSpeed => CalculateAttackSpeed();
    public int AttackDamage => CalculateWeaponDamage();
    public float MoveSpeedIncrease => CalculateMoveSpeed();
    public float CritChance => CalculateCritChance(); // called in Fighter
    public float DodgeChance => CalculateDodgeChance();  // called in Health
    
    public float LuckChance => CalculateLuckChance(); //called in LootTable

    public float ToughnessDamageReduction => CalculateToughnessDamageReduction();

    public float StatManipulation(int baseValue, float attribute, float levelMultiplier){
        return (baseValue * (1 + attribute * levelMultiplier));
    }

    float StatManipulation(float baseValue, float attribute, float levelMultiplier){
        return (baseValue * (1 + (attribute * levelMultiplier)));
    }

    float CalculateAttackSpeed() => StatManipulation(attackSpeed, dexterity, lowImpactMultiplier);
    

    float CalculateMoveSpeed() => dexterity * movementSpeedMultiplier;
    
    float CalculateCritChance() => Luck * lowImpactMultiplier;
    
    float CalculateDodgeChance() => reflex * dodgeImpactLevelMultiplier;
    

    float CalculateToughnessDamageReduction() => Toughness * toughnessDamageReductionMultiplier;
    
    
    float CalculateLuckChance() => Luck * lowImpactMultiplier;
    

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

    public void AddStats(float toughnessBuff, float strengthBuff, float dexterityBuff, float knowledgeBuff, float reflexBuff, float luckBuff){
        Toughness += toughnessBuff;
        Strength += strengthBuff;
        Dexterity += dexterityBuff;
        Knowledge += knowledgeBuff;
        Reflex += reflexBuff;
        Luck += luckBuff ;
    }
    public void AddToughness(int amount) => Toughness += amount;
    public void AddStrength(int amount) => Strength += amount;
    public void AddDexterity(int amount) => Dexterity += amount;
    public void AddKnowledge(int amount) => Knowledge += amount;
    public void AddReflex(int amount) => Reflex += amount;
    public void AddLuck(int amount) => Luck += amount;


#if UNITY_EDITOR
    void OnDrawGizmosSelected(){
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up,AttackRange);
        Handles.color = Color.gray;
        Handles.DrawWireDisc(transform.position,transform.up,interactRange);
    }
#endif
}
