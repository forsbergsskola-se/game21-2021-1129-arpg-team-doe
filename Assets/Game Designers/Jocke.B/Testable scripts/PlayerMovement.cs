using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference invalidSound;
    [SerializeField] FMODUnity.EventReference validSound;

    FMOD.Studio.EventInstance _moveInstance;
    
    
    Movement _navmeshMover;
    Statistics _statistics;
    RaycastHit hit;
    float _interactionRange;
    
 
   Vector3 _distanceToTarget;
    void Start(){
        _navmeshMover = GetComponent<Movement>();
        _statistics = GetComponent<Statistics>();
        _interactionRange = _statistics.InteractRange;

        _moveInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Move");
        
        _moveInstance.setVolume(50f);


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
                    _moveInstance.setParameterByName("MoveFeedback", 0f);
                    
                    //_moveInstance.release();
                    _navmeshMover.Mover(hit.point);
                }
                else{
                     _navmeshMover.Mover(hit.point - _distanceToTarget.normalized * 1);
                     _moveInstance.setParameterByName("MoveFeedback", 1f);
                }
                Debug.Log(hit.transform.tag);
            }
        }
    }
}
