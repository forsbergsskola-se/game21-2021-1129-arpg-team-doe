using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JB_Movement : MonoBehaviour
{
    internal NavMeshAgent _navMeshAgent;
    NavMeshPath _path;
    internal bool pathFound;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
    }

    public void Mover(Vector3 target){
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, _path);
        pathFound= _path.status == NavMeshPathStatus.PathComplete;
        if (pathFound){
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = target;
        }
        else{
            StopMoving();
            Debug.Log("no path");
        }
    }

    public void StopMoving(){
        _navMeshAgent.isStopped = true;
    }
}
