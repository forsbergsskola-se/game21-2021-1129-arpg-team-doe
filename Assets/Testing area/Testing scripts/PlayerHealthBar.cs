using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IHealthbar//, IHealthListener
{
    public Slider slider;
    public Health _health;
    int currentHealth;
    bool _isSliderShown;


    void Start(){
        //slider.maxValue = _health.ModifiedMaxHP;
    }

    void Update(){
        if (_isSliderShown){
            SetSliderCurrentHealth(_health.CurrentHP);
            return;
        }
        if (!_isSliderShown){
            _isSliderShown = true;
            SetSliderMaxHealth(_health.ModifiedMaxHP);
        }
    }

    void SetSliderMaxHealth(int maxHp){
        slider.maxValue = maxHp;
        slider.value = maxHp;
    }

    public void SetSliderCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }

    // public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
    //     SetSliderCurrentHealth(currentHealth);
    // }
}
