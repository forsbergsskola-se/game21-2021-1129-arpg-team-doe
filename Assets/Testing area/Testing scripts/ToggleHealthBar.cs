using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleHealthBar : MonoBehaviour{

    [SerializeField] GameObject _healthBar; //the active enemies HPbar only. needs combat implemented first. //wrong capitalization and maybe call it _healthBar
    bool inPursuit;
    
    void Start(){
        _healthBar.SetActive(false);
    }
    void OnMouseOver(){
        _healthBar.SetActive(true);
    }

    async void OnMouseExit(){
        await Task.Delay(2000);
        _healthBar.SetActive(false);
    }
}
