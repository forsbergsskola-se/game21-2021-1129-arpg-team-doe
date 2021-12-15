using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class UnlockDoor : MonoBehaviour, Iinteractable{
    
    [SerializeField]bool _locked = true;
    
    DoorConditions _doorConditions; 
    CursorOnDoor _cursorOnDoor;
    BoxCollider _collider;
    Animator _animator;
    EventInstance _doorInstance;
    
    
    bool _conditionCompleted;

    void Start(){
        _doorConditions = FindObjectOfType<DoorConditions>();
        _cursorOnDoor = FindObjectOfType<CursorOnDoor>();
        _collider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _doorInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Door");

    }

    void Update(){
        if (!_locked){
            _conditionCompleted = true;
            _cursorOnDoor.openable = true;
        }
        else{
            _conditionCompleted = _doorConditions.Completed;
            LockingMechanism();
            _cursorOnDoor.openable = _locked;
        }

        
    }

    void LockingMechanism(){
        _locked = !_conditionCompleted;
    }

    public void Use(){
        if (_locked){
            PlayDoorSound(1f);
        }
        else if (!_locked){
            OpenDoor();
        }
    }

    void OpenDoor(){
        PlayDoorSound(0f);
        _collider.enabled = false;
        _animator.enabled = true;
    }
    
    void PlayDoorSound(float parameter){
        _doorInstance.setParameterByName("OpenLocked", parameter);
        _doorInstance.start();
        
        //_doorInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
