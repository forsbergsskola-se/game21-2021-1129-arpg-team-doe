using UnityEngine;
using UnityEngine.AI;

public class FD_PlayerMovement : MonoBehaviour
{
    FD_NavmeshMover _navmeshMover;
    void Start(){
        _navmeshMover = GetComponent<FD_NavmeshMover>();
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)){
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
