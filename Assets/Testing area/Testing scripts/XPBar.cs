using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] LevelingGameObject playerXP;
    public Slider slider;

    void Start(){
        playerXP = GetComponent<LevelingGameObject>();
    }

    public void SetSliderXP(){
        slider.maxValue = playerXP.requiredXPInt;
        slider.minValue = 0;
        slider.value = playerXP.currentXP;
    }
}
