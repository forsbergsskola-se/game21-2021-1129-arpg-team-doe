using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Movement _navmeshMover;
    Statistics _statistics;
    RaycastHit hit;
    float _interactionRange;
 
   Vector3 _distanceToTarget;
    void Start(){
        _navmeshMover = GetComponent<Movement>();
        _statistics = GetComponent<Statistics>();
        _interactionRange = _statistics.InteractRange;
    }

    void Update(){
        MoveToCursor();
        _distanceToTarget = hit.point - transform.position ;
        
        // if (_interactionRange > _distanceToTarget){
        //     _navmeshMover.StopMoving();
        // }
    }

    void MoveToCursor(){
        if (Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit){
                if (hit.transform == CompareTag("Ground")){
                   
                   _navmeshMover.Mover(hit.point);
                }
                else{
                     _navmeshMover.Mover(hit.point - _distanceToTarget.normalized * 1);
                }
                Debug.Log(hit.transform.tag);
            }
        }
    }
}
