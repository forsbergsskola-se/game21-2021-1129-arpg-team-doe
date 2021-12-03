using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class UnlockDoor : MonoBehaviour{

    [SerializeField] float actionRange;
    
    NavMeshAgent player;
    JD_Conditioner _jdConditioner; //Change name: Conditions
    JD_CursorOnDoor _jdCursorOnDoor;
    
    bool locked = true;
    bool conditionCompleted;
    float distance;

    void Start(){
        player = FindObjectOfType<NavMeshAgent>();
        _jdConditioner = FindObjectOfType<JD_Conditioner>();
        _jdCursorOnDoor = FindObjectOfType<JD_CursorOnDoor>();
    }

    void Update(){
        conditionCompleted = _jdConditioner.completed;
        LockingMechanism();
        _jdCursorOnDoor.openable = locked;
        distance = Vector3.Magnitude(player.destination); //TODO:Replace with Detection from Detection script
    }

    void LockingMechanism(){
        if (conditionCompleted){
            locked = false;
        }else{
            locked = true;
        }
    }

    void OnMouseDown(){
        if (!locked && distance > actionRange){
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
