using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class JD_UI_Interaction : MonoBehaviour{

    [SerializeField] GameObject hpbar; //the active enemies HPbar only. needs combat implemented first.
    bool pursuing = false;

    void Start(){
        hpbar.SetActive(false);
        //pursuing = FindObjectOfType<FD_EnemyMovement>().isPursuing; <-- this is the correct logic?
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.P)){ // put real pursuit logic here.
            pursuing = !pursuing;
            if (!pursuing){
                hpbar.SetActive(false);
            }
        }
        while (pursuing){
            hpbar.SetActive(true);
            break;
        }
    }

    void OnMouseOver(){
        if (pursuing){
            return;
        }
        hpbar.SetActive(true);
    }

    async void OnMouseExit() {
        await Task.Delay(2000);
        hpbar.SetActive(false);
    }
}
