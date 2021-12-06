using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Texture2D validClickTexture;
    [SerializeField] Texture2D invalidClickTexture;
    
    [SerializeField] Texture2D standardCursorTexture;

    
    FMOD.Studio.EventInstance _moveInstance;
    Movement _navmeshMover;
    Statistics _statistics;
    Animator _animator;
    RaycastHit hit;
    
    string _currentState;
    float _interactionRange;
    bool hasPlayedSound;
    bool hasWaitedForTime;
    Vector3 _distanceToTarget;

    const string PLAYER_RUN = "playerRun";
    const string PLAYER_WALK = "playerWalk";
    void Start(){
        _navmeshMover = GetComponent<Movement>();
        _statistics = GetComponent<Statistics>();
        _interactionRange = _statistics.InteractRange;

        _animator = GetComponentInChildren<Animator>();
         _moveInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Move");

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
                
                //Hits Ground
                if (hit.transform.tag == "Ground"){
                    PlayMoveFeedback(0f);
                    //_moveInstance.release();
                    _navmeshMover.Mover(hit.point);
                    ChangeAnimationState(PLAYER_RUN);
                    if (_navmeshMover.pathFound){
                        StartCoroutine(ChangeCursorTemporary(validClickTexture,1f));
                    }
                    
                    
                }
                // else if (hit.transform.tag != "Ground" ){
                //     PlayMoveFeedback(1f);
                //     
                //     ChangeAnimationState(PLAYER_WALK);
                // }
                
                
                //Hits something
                else{
                    PlayMoveFeedback(1f);
                    _navmeshMover.Mover(hit.point - _distanceToTarget.normalized * 1);
                     ChangeAnimationState(PLAYER_WALK);
                     StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
                }
                Debug.Log(hit.transform.tag);
            }
            else if(!hasHit){
                _navmeshMover.StopMoving();
                PlayMoveFeedback(1f);
                StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
                ChangeAnimationState(PLAYER_WALK);
            }
            
        }
        else if (Input.GetMouseButtonUp(0)){
            _moveInstance.stop(STOP_MODE.ALLOWFADEOUT);
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

    void ChangeAnimationState(string newState){
        if (_currentState == newState) return;
        _animator.Play(newState);
        _currentState = newState;
    }

    IEnumerator ChangeCursorTemporary(Texture2D texture2D,float variable){
        Debug.Log("AAAAAAh i went in");
        Cursor.SetCursor(texture2D, Vector2.zero,CursorMode.ForceSoftware);
        yield return new WaitForSeconds(variable) ;
        Cursor.SetCursor(standardCursorTexture, Vector2.zero,CursorMode.ForceSoftware);
    }
}
