using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class UnlockDoor : MonoBehaviour, Iinteractable{
    
    DoorConditions _doorConditions; 
    CursorOnDoor _cursorOnDoor;
    BoxCollider _collider;
    Animator _animator;
    FMOD.Studio.EventInstance _doorInstance;
    
    bool _locked = true;
    bool _conditionCompleted;
    bool _hasPlayedSound;

    void Start(){
        _doorConditions = FindObjectOfType<DoorConditions>();
        _cursorOnDoor = FindObjectOfType<CursorOnDoor>();
        _collider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;

    }

    void Update(){
        _conditionCompleted = _doorConditions.Completed;
        LockingMechanism();
        _cursorOnDoor.openable = _locked;
    }

    void LockingMechanism(){
        _locked = !_conditionCompleted;
    }

    public void Use(){
        if (_locked){
            PlayDoorSound(1f);
            _hasPlayedSound = false;
        }
        else if (!_locked){
            OpenDoor();
            PlayDoorSound(0f);
            _hasPlayedSound = false; 
        }
    }

    void OpenDoor(){
        PlayDoorSound(0f);
        _collider.enabled = false;
        _animator.enabled = true;
    }
    
    void PlayDoorSound(float parameter){
        if (_hasPlayedSound == false){
            _doorInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Door");
            _doorInstance.setParameterByName("OpenLocked", parameter);
            _doorInstance.start();
            _hasPlayedSound = true;
        }
    }
}
