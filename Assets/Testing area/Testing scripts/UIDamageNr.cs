using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIDamageNr : MonoBehaviour{

    [SerializeField] float duration;
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;

    string dmgText;
    int dmg;
    bool activeTimer;
    bool takingDamage;
    Statistics _stats;
    
    Animator _animator;
    string _text;

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        _text = _textMeshProUGUI.text;
        _stats = GetComponentInParent<Statistics>();
    }

    void Update(){
        takingDamage = _stats.dealingDmg;
        CollectDmg(dmg);

        if (takingDamage){
            Timer();
            if (duration > 0){
                _textMeshProUGUI.text= dmgText;
                _animator.Play("FloatingPoint");
            }
            else if (!takingDamage){
                _textMeshProUGUI.text = "";
            }
        }
        takingDamage = false;
    }
    void CollectDmg(int damage){ //used for Debug
        dmgText = Convert.ToString(dmg);
        Timer();
        SetAndPlayText();
    } 

    public void Display(){
      //  activeTimer = true;
        //duration = 1;
    }
   

    void End(){
        activeTimer = false;
        takingDamage = false;
        ClearText();
    }

    
    async void Timer(){
        //Converting milliseconds to seconds.
        await Task.Delay((int)duration*1000);
        End();
    }

    void SetAndPlayText(){
        _textMeshProUGUI.text = dmgText;
        _animator.Play("FloatingPoint");
    }

    void ClearText(){
        _textMeshProUGUI.text = "";
    }
}
