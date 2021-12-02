using UnityEngine;
using UnityEngine.AI;

public class FD_PlayerMovement : MonoBehaviour
{
    FD_Movement _navmeshMover;
    void Start(){
        _navmeshMover = GetComponent<FD_Movement>();
    }

    void Update(){
        MoveToCursor();
    }

    void MoveToCursor(){
        if (Input.GetMouseButton(0)){ //if doesnt have to be in update, could be in the MoveToCursor method?
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.point != null){ _navmeshMover.Mover(hit.point);
            }
        }
    }
}
