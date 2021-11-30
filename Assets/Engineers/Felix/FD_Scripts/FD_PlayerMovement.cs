using System;
using UnityEngine;
using UnityEngine.AI;

public class FD_PlayerMovement : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;

    void Start(){
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update(){
        if (Input.GetMouseButton(0)){
            Mover();
        }
    }

    void Mover(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        _navMeshAgent.destination = hit.point;
    }
}
