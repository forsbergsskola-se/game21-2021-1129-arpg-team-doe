using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JB_Destruction : MonoBehaviour{
    
    [SerializeField] int health;

    JB_Statistics _statistics;

    float distance;
    

    void Update(){
        distance = Vector3.Distance(this.transform.position, FindObjectOfType<JB_PlayerMovement>().transform.position);
    }

    void OnMouseUpAsButton(){
        DealDamage();
    }

    void DealDamage(){ //Used for Debug
        if (_statistics.InteractRange > distance){

            health = 0; //put real logic here
        }
        Death();
    }

    void Death(){ //Used for Debug
        if (health <= 0){
            // GetComponent<Animator>().enabled = true; <- mockup
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
