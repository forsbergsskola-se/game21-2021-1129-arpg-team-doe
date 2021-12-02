using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JD_Destruction : MonoBehaviour{
    [SerializeField] float actionRange;
    [SerializeField] float health; //Int

    float distance;
    

    void Update(){
        distance = Vector3.Distance(this.transform.position, FindObjectOfType<JD_PlayerMovement>().transform.position);
    }

    void OnMouseUpAsButton(){
        DealDamage();
    }

    void DealDamage(){ //Should maybe be called TakeDamage? Objects should take damage and actors should deal damage(and also take it ofc)
        if (actionRange > distance){
            health = 0; //put real logic here
        }
        Death();
    }

    void Death(){ //Maybe rename to Die? Even tho i like the Shaketh of Spears vibe
        if (health <= 0){
            // GetComponent<Animator>().enabled = true; <- mockup
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
