using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JD_Destruction : MonoBehaviour{
    [SerializeField] float actionRange;

    float distance;

    void Update(){
        distance = Vector3.Distance(this.transform.position, FindObjectOfType<JD_PlayerMovement>().transform.position);
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
