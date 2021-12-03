using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destruction : MonoBehaviour{
    [SerializeField] int health;

    PlayerMovement _playerMovement;
    Statistics _statistics; //Used for Debug

    float distance;
    float actionRange;

    void Start(){
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _statistics = FindObjectOfType<Statistics>(); //Used for Debug
    }

    void Update(){
        distance = Vector3.Distance(this.transform.position, _playerMovement.transform.position);
    }

    void OnMouseUpAsButton(){
        DealDamage();
    }

    void DealDamage(){ //Used for Debug
        if (_statistics.ActionRange > distance){
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
