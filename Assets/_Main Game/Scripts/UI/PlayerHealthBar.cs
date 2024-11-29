using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour{
    [SerializeField] TextMeshProUGUI healthValues;
    public Slider slider;
    public Health health;
    bool _isSliderShown;

    int cachedCurrentHealth;

    void Start(){
        cachedCurrentHealth = health.CurrentHP;
        SetHealthValuesText(health.CurrentHP, health.ModifiedMaxHP);
    }

    void Update(){
        if (cachedCurrentHealth != health.CurrentHP){
            SetHealthValuesText(health.CurrentHP, health.ModifiedMaxHP);
            SetSliderMaxHealth(health.ModifiedMaxHP);
        }
        if (_isSliderShown){
            SetSliderCurrentHealth(health.CurrentHP);
        }
        if (!_isSliderShown){
            _isSliderShown = true;
            SetSliderMaxHealth(health.ModifiedMaxHP);
        }
    }

    void SetSliderMaxHealth(int maxHp){
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }

    void SetSliderCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }

    void SetHealthValuesText(int currentHealth, int maxHealth){
        healthValues.text = currentHealth + "/" + maxHealth;
    }
}