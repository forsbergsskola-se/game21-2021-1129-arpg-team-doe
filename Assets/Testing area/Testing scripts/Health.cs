using System;
using System.Collections;
using System.Collections.Generic;
using CustomLogs;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Random = System.Random;

public interface IDamageReceiver{
    void ReceiveDamage(int damage, bool isCrit, bool isPlayer);
}

public interface IHealthListener{
    void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive);
}

public class Health : MonoBehaviour, IDamageReceiver{

    // unfinished FMOD implementation
    FMOD.Studio.EventInstance instance;
    FMOD.Studio.PARAMETER_ID fmodParameterID;
    public FMODUnity.EventReference fmodEvent;
    [SerializeField] [Min(0)] float parameter;
    
    [SerializeField] internal int regenerateThreshold = 80;
    [SerializeField] int healthRegen = 10;
    
    EventInstance _takeDamage;
    public EventReference TakeDamageReference;
    // remove if unsuccessful

    //[SerializeField] Fmod event - Having this public or serialized doesnt work

    [SerializeField] int maxHP = 100;

    [SerializeField] XPDrop _xpDrop;
    [SerializeField] XPDropEvent _xpDropEvent;
    Statistics _stats;
    Random random;
    //List<IDamageNumbers> damageNumbersList;

    public bool isRegenerating;
    public bool IsAlive => CurrentHP > 0;
    public int CurrentHP{ get; private set; }
    public int ModifiedMaxHP => CalculateMaxHP();

    void Start(){
        _stats = GetComponent<Statistics>();
        random = new Random();
        CurrentHP = ModifiedMaxHP;
        _takeDamage = RuntimeManager.CreateInstance(TakeDamageReference);

        if (_xpDrop != null) {
            _xpDrop = GetComponent<XPDrop>();
        }

        // if (_xpDropEvent != null) {
        //     _xpDropEvent = GetComponent<XPDropEvent>();
        // }

            // unfinished FMOD implementation
        FMODEvent();
    }

    // unfinished FMOD implementation
    void FMODEvent() {
        if (!fmodEvent.IsNull){
            instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        }

        FMOD.Studio.EventDescription parameterEventDescription;
        instance.getDescription(out parameterEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
        parameterEventDescription.getParameterDescriptionByName("Parameter", out parameterDescription);
        fmodParameterID = parameterDescription.id;
        instance.setParameterByID(fmodParameterID, parameter);
    }

    public void UpdateHealth(int healthChange){ //What about healing
        CurrentHP += healthChange;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, ModifiedMaxHP);
    }
    
    public IEnumerator HealthRegeneration(){ // health regeneration seems weird
        isRegenerating = true;
        while(CurrentHP <= regenerateThreshold){
                yield return new WaitForSeconds(1f);
                UpdateHealth(healthRegen);
        }
        isRegenerating = false;
        yield return null;
    }

    int CalculateMaxHP(){
        return (int) _stats.StatManipulation(maxHP, _stats.Toughness, _stats.highImpactLevelMultiplier);
    }

    public void ReceiveDamage(int damage, bool isCrit, bool isPlayer){ //Toughness should affect this
        damage = ProcessDamage(damage);
        UpdateHealth(-damage);
        if (gameObject.tag == "Player"){
            PlaySound();
        }
        //this.LogTakeDamage(damage,CurrentHP);
        if (!IsAlive){
            OnDeath(isPlayer);
        }

        foreach(var healthListener in GetComponentsInChildren<IHealthListener>()){
            healthListener.HealthChanged(CurrentHP, ModifiedMaxHP, damage, isCrit, IsAlive);
        }
        PlaySound();
    }

    bool DodgeSuccessful(){
        return random.NextDouble() < _stats.DodgeChance;
    }

    int ProcessDamage(int dmg){
        if (DodgeSuccessful()){
            dmg = 0;
        }
        //Debug.Log(transform.name + " receives " + dmg + " Damage");
        return dmg;
    }

    //This calls the event that you put in if you put it in, if there's no event, nothing happens.
    void OnDeath(bool isPlayer){
        //Debug.Log(isPlayer);
        //Check if event is not null and is not player, and attacker is player, call event. OR if event is not null and is player, call event.
        if (_xpDropEvent != null && this.gameObject.tag != "Player" && isPlayer || _xpDropEvent != null && this.gameObject.tag == "Player"){
            _xpDropEvent.Invoke(_xpDrop.xpAmount);
        }
    }

    public void PlaySound(){
        _takeDamage.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _takeDamage.start();  
        }
    }
}
