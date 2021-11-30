using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JD_Destruction : MonoBehaviour{
    [SerializeField] float actionRange;
    [SerializeField] float health;

    float distance;

    void Update(){
        distance = Vector3.Distance(this.transform.position, FindObjectOfType<JD_PlayerMovement>().transform.position);
    }

    void OnMouseUpAsButton(){
        DealDamage();
    }

    void DealDamage(){
        if (actionRange > distance){
            health = 0; //put real logic here
        }
        Death();
    }

    void Death(){
        if (health <= 0){
            // GetComponent<Animator>().enabled = true; <- mockup
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
