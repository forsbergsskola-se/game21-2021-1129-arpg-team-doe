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
        Mover();
    }

    void Mover(){
        if (Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            _navMeshAgent.destination = hit.point;
        }
    }
}
