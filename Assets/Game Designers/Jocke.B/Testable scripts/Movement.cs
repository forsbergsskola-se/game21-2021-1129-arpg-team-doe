using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    NavMeshPath _path;
    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
    }

    public void Mover(Vector3 target){
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, _path);
        bool pathFound = _path.status == NavMeshPathStatus.PathComplete;
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
