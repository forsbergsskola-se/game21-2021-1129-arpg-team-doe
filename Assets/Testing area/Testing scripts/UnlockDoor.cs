using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public interface IInteractSound{
    void PlaySound(float parameter){
        PLAYBACK_STATE playback_state;
    }
}

public class UnlockDoor : MonoBehaviour, Iinteractable,IInteractSound{
    
    [SerializeField]bool _locked = true;
    
    DoorConditions _doorConditions; 
    CursorOnDoor _cursorOnDoor;
    BoxCollider _collider;
    Animator _animator;
    EventInstance _doorInstance;
    public FMODUnity.EventReference DoorReference;
    
    
    bool _conditionCompleted;

    void Start(){
        _doorConditions = FindObjectOfType<DoorConditions>();
        _cursorOnDoor = FindObjectOfType<CursorOnDoor>();
        _collider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        
        _doorInstance = FMODUnity.RuntimeManager.CreateInstance(DoorReference);

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
            PlaySound(1f);
        }
        else if (!_locked){
            OpenDoor();
        }
    }

    void OpenDoor(){
        PlaySound(0f);
        _collider.enabled = false;
        _animator.enabled = true;
    }

    public void PlaySound(float parameter){
        _doorInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _doorInstance.setParameterByName("OpenLocked", parameter);
            _doorInstance.start();  
        }
    }
}
