using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{

    Statistics _statistics;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void Start(){
        _statistics = GetComponentInParent<Statistics>();
        var maxHP = _statistics.maxHP;
        SetSliderMaxHealth(maxHP); 
    }

    void Update(){
        SetSliderHealth();
    }

    public void SetSliderMaxHealth(float maxHP){
        slider.maxValue = maxHP;
        slider.value = maxHP;

        // puts the health bar gradient to green
        fill.color = gradient.Evaluate(1f);
    }

    public void SetSliderHealth(){
        var currentHealth = _statistics.currentHP;
        slider.value = currentHealth;

        // changes the health bar gradient to whatever the health is
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
