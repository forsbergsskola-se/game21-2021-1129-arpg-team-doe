using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JD_UI_Interaction : MonoBehaviour{

    void Start(){
        GetComponentInChildren<JD_HealthBar>().gameObject.SetActive(false);
    }

    void OnMouseEnter(){
        GetComponentInChildren<JD_HealthBar>().gameObject.SetActive(true);
    }

    void OnMouseExit(){
        GetComponentInChildren<JD_HealthBar>().gameObject.SetActive(false);
    }
}
