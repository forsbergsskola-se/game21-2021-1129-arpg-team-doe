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
        bool pathFound = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, _path);
        if (pathFound){
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = target;
        }
        else{
            StopMoving();
        }
    }

    public void StopMoving(){
        _navMeshAgent.isStopped = true;
    }
}
