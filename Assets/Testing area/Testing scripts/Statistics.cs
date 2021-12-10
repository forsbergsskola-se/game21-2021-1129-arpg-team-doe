using UnityEngine;

public class Statistics : MonoBehaviour{

    int weaponDamage = 1; // for debug
    bool isRanged;
    int damage;

    [SerializeField]
    float toughness, strength, dexterity, knowledge, reflex, luck, interactRange, attackRange, attackSpeed;

    float dodgeChance;

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

    public float DodgeChance{
        get => dodgeChance;
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

    public float AttackSpeed{
        get { return attackSpeed; }
        private set { attackSpeed = value * (this.dexterity * 0.01f + 1); }
    }

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
    }

    void Start(){
        currentHP = maxHP;
    }

    // maybe change later
    public void UpdateHealth(int healthChange){
        currentHP -= healthChange;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    float AttacksPerSecond(){
        return attackSpeed * (this.dexterity * 0.01f + 1);
    }

    float CalculateDodgeChance(){
        return dodgeChance = this.reflex * 0.01f;
    }

    int CalculateWeaponDamage(int weaponDamage){
        float damageMultiplier = 0f;
        if (isRanged){
            damageMultiplier = (this.dexterity * 0.01f + 1);
        }
        if (!isRanged){
            damageMultiplier = (this.strength * 0.01f + 1);
        }
        return damage = (int) (weaponDamage * damageMultiplier);
    }

    float DamageMultiplier(){
        float damageMultiplier;
        if (isRanged){
            return damageMultiplier = (this.dexterity * 0.01f + 1);
        }
        return damageMultiplier = (this.strength * 0.01f + 1);
    }
}
