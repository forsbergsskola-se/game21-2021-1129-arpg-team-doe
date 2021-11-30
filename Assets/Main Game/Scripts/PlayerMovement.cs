using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    NavMeshPath _path;

    void Start(){
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Mover();
        }
    }

    void Mover(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        bool pathFound = NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, _path);
        if (pathFound){
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = hit.point;
        }
        else{
            _navMeshAgent.isStopped = true;
        }
    }
}
