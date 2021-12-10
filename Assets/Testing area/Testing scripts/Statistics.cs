using System;
using UnityEngine;

public class Statistics : MonoBehaviour{



    [SerializeField]
    float toughness, strength, dexterity, knowledge, reflex, luck, interactRange, attackRange, attackSpeed;

    [SerializeField] int weaponDamage = 10; // for debug
    [SerializeField] int defaultDamage = 5;
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

    public int maxHP;
    public int currentHP { get; private set; }

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
    
    public bool IsAlive => currentHP > 0;
    public bool dealingDmg; //used for debug

    public Statistics(float toughness, float strength, float dexterity, float knowledge, float reflex, float luck,
        float interactRange, float attackRange, float attackSpeed, int maxHp, bool dealingDmg) {
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
        damage = defaultDamage;
    }

    void Start(){
        currentHP = maxHP;
    }

    // maybe change later
    public void UpdateHealth(int healthChange){
        currentHP -= healthChange;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    float CalculateAttackSpeed(){
        return (dexterity * 0.005f + 1) * attackSpeed;
    }

    float CalculateCritChance(){
        return luck * 0.005f;
    }
    float CalculateDodgeChance(){
        return reflex * 0.005f;
    }
    int CalculateWeaponDamage(){
        float damageMultiplier = 0f;
        if (isRanged){
            damageMultiplier = dexterity * 0.005f + 1;
        }
        else{
            damageMultiplier = strength * 0.005f + 1;
        }
        return damage = (int) (weaponDamage * damageMultiplier);
    }



    // we don't need this?
    float DamageMultiplier(){
        float damageMultiplier;
        if (isRanged){
            return damageMultiplier = (this.dexterity * 0.01f + 1);
        }
        return damageMultiplier = (this.strength * 0.01f + 1);
    }
}
