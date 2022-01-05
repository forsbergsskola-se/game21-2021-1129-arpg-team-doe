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
            if (enemy.distance > VARIABLE.distance){
                enemy = VARIABLE;
            }
        }
        var dist = enemy.isActiveAndEnabled ? enemy.distance : 20f;
        var fighting = dist < 7;
        // Debug.Log($"Im the distance: {dist}");
        // Debug.Log($"Im in combat: {fighting}");
        Music.setParameterByName("DistanceFromEnemy", dist);
        Music.setParameterByName("InCombat", dist < 7 ? 1 : 0);
    }

    public void Progress(){
        Music.setParameterByName("PlayerProgress", soundIncrement);
    }

    void OnDestroy(){
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
