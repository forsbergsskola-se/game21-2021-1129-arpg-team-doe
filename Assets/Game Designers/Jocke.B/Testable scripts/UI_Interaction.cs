using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Interaction : MonoBehaviour{

    [SerializeField] GameObject healthBar; //the active enemies HPbar only. needs combat implemented first. //wrong capitalization and maybe call it healthBar
    bool pursuing;

    void Start(){
        healthBar.SetActive(false);
        //pursuing = FindObjectOfType<FD_EnemyMovement>().isPursuing; <-- this is the correct logic?
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){ //used for Debug
            pursuing = !pursuing;
            if (!pursuing){
                healthBar.SetActive(false);
            }
        }
        while (pursuing){
            healthBar.SetActive(true);
            break;
        }
    }

    void OnMouseOver(){
        if (pursuing){
            return;
        }
        healthBar.SetActive(true);
    }

    async void OnMouseExit(){
        await Task.Delay(2000);
        healthBar.SetActive(false);
    }
}
