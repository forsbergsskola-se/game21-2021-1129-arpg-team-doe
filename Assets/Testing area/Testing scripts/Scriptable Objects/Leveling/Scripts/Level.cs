using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class Level : MonoBehaviour
{
    [SerializeField] public LevelingGameObject LevelingGameObject;
    [SerializeField] public FMODUnity.EventReference fmodEvent;
    
    FMOD.Studio.EventInstance instance;
    void Start(){
        if (!fmodEvent.IsNull){
            instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        }
    }
    public void PlaySound(){
        instance.start();
        instance.release();
    }
}
