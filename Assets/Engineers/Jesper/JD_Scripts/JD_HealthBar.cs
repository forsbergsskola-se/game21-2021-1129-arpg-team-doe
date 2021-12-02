using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JD_HealthBar : MonoBehaviour{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void Start(){
        var maxHP = GetComponentInParent<JD_EnemyStats>().maxHP;
        SetMaxHealth(maxHP); 
    }

    // void OnMouseOver(){
    //     this.gameObject.SetActive(true);
    // }

    void Update(){
        SetHealth();
    }

    public void SetMaxHealth(float maxHP){ //We should rename this (probably SetSliderMaxHealth)
        slider.maxValue = maxHP;
        slider.value = maxHP;

        // puts the health bar gradient to green
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(){
        var currentHealth = GetComponentInParent<JD_EnemyStats>().currentHP;
        slider.value = currentHealth;

        // changes the health bar gradient to whatever the health is
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
