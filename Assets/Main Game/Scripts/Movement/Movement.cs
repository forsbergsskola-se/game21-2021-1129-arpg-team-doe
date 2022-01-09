using System;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour{
    [SerializeField] float maxSpeed = 8f;
    [SerializeField] FMODUnity.EventReference movementSound;

    EventInstance _movementInstance;
    NavMeshPath _path;
    internal NavMeshAgent _navMeshAgent;
    internal bool pathFound;
    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        _movementInstance = FMODUnity.RuntimeManager.CreateInstance(movementSound);
    }

    

    public void Mover(Vector3 target, float speedFraction){
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, _path);
        pathFound= _path.status == NavMeshPathStatus.PathComplete;
        if (pathFound){
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = target;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
           PlayMovementSound();
        }
        else{
            StopMoving();
            StopMovementSound();
        }
    }

    public void StopMoving(){
        _navMeshAgent.isStopped = true;
    }

    void PlayMovementSound(){
        _movementInstance.getPlaybackState(out var playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED){
                _movementInstance.start();  
            }
    }

    public void StopMovementSound(){
        _movementInstance.stop(STOP_MODE.IMMEDIATE);
    }
}
