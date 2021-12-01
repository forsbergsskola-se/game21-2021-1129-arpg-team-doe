using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class JD_UI_Interaction : MonoBehaviour{

    [SerializeField] GameObject hpbar;
    bool pursuing = false;

    void Start(){
        hpbar.SetActive(false);
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

    void OnMouseEnter(){
        if (pursuing){
            return;
        }
        hpbar.SetActive(true);
    }

    void OnMouseExit(){
        Thread.Sleep(2000);
        hpbar.SetActive(false);
    }
}
