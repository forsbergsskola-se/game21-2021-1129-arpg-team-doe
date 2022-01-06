using System;
using System.Collections;
using CustomLogs;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Random = System.Random;

public interface IDamageReceiver{
    void ReceiveDamage(int damage, bool isCrit, bool isPlayer, DamageType receivedDmgType);
}

public interface IHealthListener{
    void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive);
}

public class Health : MonoBehaviour, IDamageReceiver{
    
    [SerializeField] [Min(0)] float parameter;
    
    [SerializeField] internal int stopRegenerateThreshold;
    [SerializeField] int healthRegen = 10;
    
    EventInstance _takeDamage;
    public EventReference TakeDamageReference;

    [SerializeField] int maxHP = 100;

    [SerializeField] XPDrop _xpDrop;
    [SerializeField] XPDropEvent _xpDropEvent;
    Statistics _stats;
    Random random;
    

    public bool isRegenerating;
    public bool IsAlive => CurrentHP > 0 && !isRegenerating;
    public int CurrentHP{ get; private set; }
    public int ModifiedMaxHP => CalculateMaxHP();

    void Awake(){
        _stats = GetComponent<Statistics>();
    }

    void Start(){
        
        random = new Random();
        CurrentHP = ModifiedMaxHP;
        _takeDamage = RuntimeManager.CreateInstance(TakeDamageReference);

        if (_xpDrop != null) {
            _xpDrop = GetComponent<XPDrop>();
        }
    }


    public void UpdateHealth(int healthChange){ 
        CurrentHP += healthChange;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, ModifiedMaxHP);
        this.LogHealth(CurrentHP);
    }
    
    public IEnumerator HealthRegeneration(){
        isRegenerating = true;
        while(CurrentHP <= stopRegenerateThreshold){
            UpdateHealth(healthRegen);
            if (CurrentHP >= stopRegenerateThreshold){
                 isRegenerating = false;
                 break;
            }
            yield return new WaitForSeconds(1f);
        }
        isRegenerating = false;
    }

    int CalculateMaxHP(){
        return (int) _stats.StatManipulation(maxHP, _stats.Toughness, _stats.highImpactLevelMultiplier);
    }

    public void ReceiveDamage(int receivedDamage, bool isCrit, bool attackerIsPlayer, DamageType receivedDmgType){ //Toughness should affect this
        var damage = ProcessDamage(receivedDamage, receivedDmgType);
        UpdateHealth(-damage);
        if (gameObject.tag == "Player"){
            PlaySound();
        }
        if (!IsAlive){
            OnDeath(attackerIsPlayer);
        }

        foreach(var healthListener in GetComponentsInChildren<IHealthListener>()){
            healthListener.HealthChanged(CurrentHP, ModifiedMaxHP, damage, isCrit, IsAlive);
        }
        PlaySound();
        this.LogHealth(CurrentHP);
    }

    bool DodgeSuccessful(){
        return random.NextDouble() < _stats.DodgeChance;
    }

    int ProcessDamage(int dmg, DamageType receivedDmgType)
    {
        if (DodgeSuccessful()){
            dmg = 0;
        }

        foreach (var damageType in _stats.vulnerabilities)
        {
            if (receivedDmgType == damageType)
            {
              dmg = Mathf.RoundToInt(dmg * _stats.vulnerabilityDamageModifier);
                break;
            }
        }
        foreach (var damageType in _stats.resistances)
        {
            if (receivedDmgType == damageType)
            {
                dmg = Mathf.RoundToInt(dmg * _stats.resistanceDamageModifier);
                break;
            }
        }
        return dmg;
    }

    //This calls the event that you put in if you put it in, if there's no event, nothing happens.
    void OnDeath(bool attackerIsPlayer){
        //Check if event is not null and is not player, and attacker is player, call event. OR if event is not null and is player, call event.
        if (_xpDropEvent != null && this.gameObject.tag != "Player" && attackerIsPlayer || _xpDropEvent != null && this.gameObject.tag == "Player"){
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
