using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Movement _navmeshMover;
    void Start(){
        _navmeshMover = GetComponent<Movement>();
    }

    void Update(){
        MoveToCursor();
    }

    void MoveToCursor(){
        if (Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit){ _navmeshMover.Mover(hit.point);
            }
        }
    }
}