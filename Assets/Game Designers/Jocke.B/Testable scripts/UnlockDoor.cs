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
    BoxCollider _collider;
    NavMeshObstacle _obstacle;
    Animator _animator;
    
    bool locked = true;
    bool conditionCompleted;
    float distance;

    void Start(){
        player = FindObjectOfType<PlayerMovement>();
        _conditions = FindObjectOfType<Conditions>();
        _cursorOnDoor = FindObjectOfType<CursorOnDoor>();
        _collider = GetComponent<BoxCollider>();
        _obstacle = GetComponent<NavMeshObstacle>();
        //Test
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        //Test
       
    }

    void Update(){
        conditionCompleted = _conditions.completed;
        LockingMechanism();
        _cursorOnDoor.openable = locked; //? Could you explain why this is in update?
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
        Debug.Log(distance + " = Distance to door" + "Locked?" +locked);
        if (!locked && distance < actionRange){
            OpenDoor();
        }
    }

    void OpenDoor(){
        _collider.enabled = false;
        _obstacle.enabled = false;
        //Test
        _animator.enabled = true;
        //Test
    }
}
