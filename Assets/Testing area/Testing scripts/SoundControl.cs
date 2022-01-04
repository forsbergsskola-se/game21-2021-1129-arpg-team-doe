using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour{
    
    FMOD.Studio.EventInstance Music;

    public float soundIncrement{ get; set; }
    
    void Start(){
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/MusicCrypt");
        Music.start();
        Music.release();
    }

    public void Progress(){
        Music.setParameterByName("PlayerProgress", soundIncrement);
    }

    void OnDestroy(){
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
