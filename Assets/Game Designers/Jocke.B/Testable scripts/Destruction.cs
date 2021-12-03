using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destruction : MonoBehaviour{
    [SerializeField] float actionRange;
    [SerializeField] int health;

    float distance;
    

    void Update(){
        distance = Vector3.Distance(this.transform.position, FindObjectOfType<PlayerMovement>().transform.position);
    }

    void OnMouseUpAsButton(){
        DealDamage();
    }

    void DealDamage(){ //Used for Debug
        if (actionRange > distance){
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