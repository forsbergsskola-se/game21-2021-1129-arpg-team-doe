using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class UnlockDoor : MonoBehaviour{

    [SerializeField] float actionRange;
    
    PlayerMovement player;
    Conditions _conditions; 
    CursorOnDoor _cursorOnDoor;
    
    bool locked = true;
    bool conditionCompleted;
    float distance;

    void Start(){
        player = FindObjectOfType<PlayerMovement>();
        _conditions = FindObjectOfType<Conditions>();
        _cursorOnDoor = FindObjectOfType<CursorOnDoor>();
    }

    void Update(){
        conditionCompleted = _conditions.completed;
        LockingMechanism();
        _cursorOnDoor.openable = locked;
        distance = Vector3.Distance(transform.position, player.transform.position); //TODO:Replace with Detection from Detection script
    }

    void LockingMechanism(){
        if (conditionCompleted){
            locked = false;
        }else{
            locked = true;
        }
    }

    void OnMouseDown(){
        if (!locked && distance < actionRange){
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
