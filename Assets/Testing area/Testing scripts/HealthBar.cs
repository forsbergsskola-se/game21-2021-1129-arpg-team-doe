using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthbar{
    void SetSliderCurrentHealth(int currentHealth);

}
public class HealthBar : MonoBehaviour, IHealthbar{
    
    Statistics _statistics;
    TakeDamage _takeDamage;
    GameObject _parent;
    
    public Slider slider;
    
    Quaternion _startRotation;

    Vector3 _offset = new Vector3(0, 2, 1);
    
    public void Start(){
        _statistics = GetComponentInParent<Statistics>();

        var maxHP = _statistics.maxHP;
        SetSliderMaxHealth(maxHP);

        _startRotation = transform.rotation;
        _parent = GetComponentInParent<ToggleHealthBar>()?.gameObject;
    }

    void Update(){
        transform.rotation = _startRotation;
        transform.position = _parent.transform.position + _offset;
    }

    void SetSliderMaxHealth(float maxHP){
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void SetSliderCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }
    
}
