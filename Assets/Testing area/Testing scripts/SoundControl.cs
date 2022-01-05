using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour{
    
    FMOD.Studio.EventInstance Music;
    EnemyMovement[] _enemyMovements;

    public float soundIncrement{ get; set; }
    
    void Start(){
        _enemyMovements = FindObjectsOfType<EnemyMovement>();
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/MusicCrypt");
        Music.start();
        Music.release();
    }

    void Update(){
        EnemyMovement enemy = _enemyMovements[0];
        foreach (var VARIABLE in _enemyMovements){
            if (VARIABLE.distance <= enemy.distance){
                enemy = VARIABLE;
            }
        }
        Music.setParameterByName("DistanceFromEnemy", enemy.distance);
        if (enemy.distance < 5){
            Music.setParameterByName("InCombat", 1);
        }
        else{
            Music.setParameterByName("InCombat", 0);
        }
    }

    public void Progress(){
        Music.setParameterByName("PlayerProgress", soundIncrement);
    }

    void OnDestroy(){
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
