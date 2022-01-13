using System.Collections;
using UnityEngine;

public class SoundControl : MonoBehaviour{
    FMOD.Studio.EventInstance _music;
    EnemyController[] _enemyMovements;
    EnemyController enemy;

    bool _fighting;
    public float SoundIncrement{ get; set; }
    
    void Start(){
        _enemyMovements = FindObjectsOfType<EnemyController>();
        _music = FMODUnity.RuntimeManager.CreateInstance("event:/MusicCrypt");
        _music.start();
        _music.release();
        enemy = _enemyMovements[0];
        InvokeRepeating(nameof(CombatMusic), 0f, 1f);
    }
    
    public void Progress(){
        _music.setParameterByName("PlayerProgress", SoundIncrement);
    }

    void CombatMusic(){
        foreach (var VARIABLE in _enemyMovements){
            if (enemy.distance > VARIABLE.distance){
                enemy = VARIABLE;
            }
        }
        var dist = enemy.isActiveAndEnabled ? enemy.distance : 20f;
        _fighting = dist < 7;
        _music.setParameterByName("DistanceFromEnemy", dist);
        _music.setParameterByName("InCombat", _fighting ? 1 : 0);
        StartCoroutine(DelayCombatExit());
    }

    IEnumerator DelayCombatExit(){
        if (_fighting){
            yield return new WaitForSeconds(2);
        }
    }

    void OnDestroy(){
        _music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
