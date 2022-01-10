using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour{
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] FMODUnity.EventReference movementSound;

    EventInstance _movementInstance;
    NavMeshPath _path;
    AnimationController _animationController;
    Statistics _statistics;
    Fighter _fighter;
    internal NavMeshAgent _navMeshAgent;
    internal bool pathFound;
    const string IDLE = "Idle";
    
    void Awake(){
        _statistics = GetComponent<Statistics>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        _animationController = GetComponentInChildren<AnimationController>();
        _fighter = GetComponent<Fighter>();
        _movementInstance = FMODUnity.RuntimeManager.CreateInstance(movementSound);
        _movementInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
    }

    

    public void Mover(Vector3 target, float speedFraction){
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, _path);
        pathFound= _path.status == NavMeshPathStatus.PathComplete;
        if (pathFound){
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = target;
            _navMeshAgent.speed = _statistics.MoveSpeedIncrease + movementSpeed * Mathf.Clamp01(speedFraction);
            PlayMovementSound();
        }
        else{
            StopMoving();
            StopMovementSound();
        }
    }

    public void StopMoving(){
        _navMeshAgent.isStopped = true;
        if (!_fighter.isAttacking){
            _animationController.ChangeAnimationState(IDLE);
        }
    }

    void PlayMovementSound(){
       // _movementInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        _movementInstance.getPlaybackState(out var playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED){
                _movementInstance.start();
            }
    }

    public void StopMovementSound(){
        _movementInstance.stop(STOP_MODE.IMMEDIATE);
    }
}
