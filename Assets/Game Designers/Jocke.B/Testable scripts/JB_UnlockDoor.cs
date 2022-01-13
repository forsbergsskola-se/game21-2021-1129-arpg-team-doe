using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class JB_UnlockDoor : MonoBehaviour{

    [SerializeField] float actionRange;
    
    JB_PlayerMovement player;
    JB_Conditions _conditions; 
    JB_CursorOnDoor _cursorOnDoor;
    BoxCollider _collider;
    NavMeshObstacle _obstacle;
    Animator _animator;
    FMOD.Studio.EventInstance _doorInstance;
    
    bool locked = true;
    bool conditionCompleted;
    bool hasPlayedSound;
    float distance;

    void Start(){
        player = FindObjectOfType<JB_PlayerMovement>();
        _conditions = FindObjectOfType<JB_Conditions>();
        _cursorOnDoor = FindObjectOfType<JB_CursorOnDoor>();
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
            PlayDoorSound(0f);
            hasPlayedSound = false;
        }
        if (locked && distance < actionRange){
            PlayDoorSound(1f);
            hasPlayedSound = false;
        }
    }

    void OpenDoor(){
        // _collider.enabled = false;
        // _obstacle.enabled = false;
        //Test
        _animator.enabled = true;
        //Test
    }
    
    void PlayDoorSound(float parameter){
        if (hasPlayedSound == false){
            _doorInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Door");
            _doorInstance.setParameterByName("OpenLocked", parameter);
            _doorInstance.start();
            hasPlayedSound = true;
        }
    }
}
