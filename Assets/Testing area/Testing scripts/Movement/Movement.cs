using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 8f;
    internal NavMeshAgent _navMeshAgent;
    NavMeshPath _path;
    internal bool pathFound;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
    }

    public void Mover(Vector3 target, float speedFraction){
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, _path);
        pathFound= _path.status == NavMeshPathStatus.PathComplete;
        if (pathFound){
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = target;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        }
        else{
            StopMoving();
            //Debug.Log("no path");
        }
    }

    public void StopMoving(){
        _navMeshAgent.isStopped = true;
    }
}
