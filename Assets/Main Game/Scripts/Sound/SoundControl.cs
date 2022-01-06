using UnityEngine;

public class SoundControl : MonoBehaviour{
    FMOD.Studio.EventInstance Music;
    EnemyController[] _enemyMovements;
    public float soundIncrement{ get; set; }
    
    void Start(){
        _enemyMovements = FindObjectsOfType<EnemyController>();
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/MusicCrypt");
        Music.start();
        Music.release();
        InvokeRepeating(nameof(CombatMusic), 0f, 1f);
    }
    
    public void Progress(){
        Music.setParameterByName("PlayerProgress", soundIncrement);
    }

    void CombatMusic(){
        EnemyController enemy = _enemyMovements[0];
        foreach (var VARIABLE in _enemyMovements){
            if (enemy.distance > VARIABLE.distance){
                enemy = VARIABLE;
            }
        }
        var dist = enemy.isActiveAndEnabled ? enemy.distance : 20f;
        var fighting = dist < 7;
        //Debug.Log($"Im the distance: {dist}");
        //Debug.Log($"Im in combat: {fighting}"); TODO: remove these if tom needs to look at it.
        Music.setParameterByName("DistanceFromEnemy", dist);
        Music.setParameterByName("InCombat", dist < 7 ? 1 : 0);
    }

    void OnDestroy(){
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
