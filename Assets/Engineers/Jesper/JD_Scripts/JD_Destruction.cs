using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JD_Destruction : MonoBehaviour{
    [SerializeField] float actionRange;

    void Update(){
        var distance = 2f;
    }

    void OnMouseUpAsButton(){
        if (actionRange < distance){
            Death();  
        }
    }

    void Death(){
        GetComponent<Animator>().enabled = true; //mockup
        GetComponent<NavMeshObstacle>().enabled = false;
    }
}
