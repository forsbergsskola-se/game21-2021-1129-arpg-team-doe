using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference invalidSound;
    [SerializeField] FMODUnity.EventReference validSound;

    FMOD.Studio.EventInstance _moveInstance;
    
    
    Movement _navmeshMover;
    Statistics _statistics;
    RaycastHit hit;
    float _interactionRange;
    bool hasPlayedSound;
    
 
   Vector3 _distanceToTarget;
    void Start(){
        _navmeshMover = GetComponent<Movement>();
        _statistics = GetComponent<Statistics>();
        _interactionRange = _statistics.InteractRange;

        
       // _moveInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Move");
        
        //_moveInstance.setVolume(50f);


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
                
                if (hit.transform.tag == "Ground"){
                    PlayMoveFeedback(0f);
                    //_moveInstance.release();
                    _navmeshMover.Mover(hit.point);
                }
                else{
                    PlayMoveFeedback(1f);
                    _navmeshMover.Mover(hit.point - _distanceToTarget.normalized * 1);
                     
                }
                Debug.Log(hit.transform.tag);
            }
        }
        else if (Input.GetMouseButtonUp(0)){
            //_moveInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _moveInstance.release();
            hasPlayedSound = false;
        }
    }

    void PlayMoveFeedback(float parameter){
        if (hasPlayedSound == false){
            _moveInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Move");
            _moveInstance.setParameterByName("MoveFeedback", parameter);
            _moveInstance.start();
            hasPlayedSound = true;
        }
    }
}
