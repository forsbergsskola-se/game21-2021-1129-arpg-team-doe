using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    Movement _navmeshMover;
    void Start(){
        _navmeshMover = GetComponent<Movement>();
    }

    void Update(){
        if (Input.GetMouseButton(0)){
            MoveToCursor();
        }
    }

    void MoveToCursor(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.point != null){
            _navmeshMover.Mover(hit.point);
        }
    }
}
