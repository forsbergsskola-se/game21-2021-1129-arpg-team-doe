using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] LevelingGameObject playerXP;
    //public Slider slider;
    public Image image;
    public float XPValue;
    
    [ContextMenu("SetSliderXP")]
    public void SetXPBar(){ 
        XPValue = ((float)playerXP.currentXP/(float)playerXP.requiredXPInt);
        image.fillAmount = XPValue;
    }
}
