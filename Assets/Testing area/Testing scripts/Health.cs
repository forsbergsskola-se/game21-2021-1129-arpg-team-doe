using System;
using System.Collections.Generic;
using CustomLogs;
using FMODUnity;
using UnityEngine;
using Random = System.Random;

public interface IHealthListener{
    void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive);
}

public class Health : MonoBehaviour, IDamageReceiver{

    FMOD.Studio.EventInstance instance;
    FMOD.Studio.PARAMETER_ID fmodParameterID;

    [FMODUnity.EventRef] public string fmodEvent;

    [SerializeField] [Min(0)] int parameter;

    [SerializeField] int maxHP = 100;

    Statistics _stats;
    Random random;
    //List<IDamageNumbers> damageNumbersList;

    public bool IsAlive => CurrentHP > 0;
    public int CurrentHP{ get; private set; }
    public int ModifiedMaxHP => CalculateMaxHP();

    void Start(){
        _stats = GetComponent<Statistics>();
        random = new Random();
        CurrentHP = ModifiedMaxHP;

        FMODEvent();
    }

    void Update() {
        instance.setParameterByID(fmodParameterID, parameter);
    }

    void FMODEvent() {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        FMOD.Studio.EventDescription parameterEventDescription;
        instance.getDescription(out parameterEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
        parameterEventDescription.getParameterDescriptionByName("Parameter", out parameterDescription);
        fmodParameterID = parameterDescription.id;

        instance.start();
    }

    public void UpdateHealth(int healthChange){
        CurrentHP -= healthChange;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, ModifiedMaxHP);
    }

    int CalculateMaxHP(){
        return (int) _stats.StatManipulation(maxHP, _stats.Toughness, _stats.highImpactLevelMultiplier);
    }

    public void ReceiveDamage(int damage, bool isCrit){ //Toughness should affect this
        damage = ProcessDamage(damage);
        UpdateHealth(damage);
        this.LogTakeDamage(damage,CurrentHP);

        foreach(var healthListener in GetComponentsInChildren<IHealthListener>()){
            healthListener.HealthChanged(CurrentHP, ModifiedMaxHP, damage, isCrit, IsAlive);
        }
    }

    // void ActivateDamageNumbers(int damage, bool isCrit){
    //     foreach (IDamageNumbers damageNumber in damageNumbersList){
    //         if (damageNumber != null){
    //             damageNumber.DisplayDmg(damage, isCrit);
    //         }
    //         else{
    //             damageNumbersList.Remove(damageNumber);
    //         }
    //     }
    // }

    bool DodgeSuccessful(){
        return random.NextDouble() < _stats.DodgeChance;
    }

    int ProcessDamage(int dmg){
        if (DodgeSuccessful()){
            dmg = 0;
        }
        Debug.Log(transform.name + " receives " + dmg + " Damage");
        return dmg;
    }
}
