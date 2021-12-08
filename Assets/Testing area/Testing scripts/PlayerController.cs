using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Texture2D validClickTexture;
    [SerializeField] Texture2D invalidClickTexture;
    [SerializeField] Texture2D standardCursorTexture;
    [SerializeField] float distanceToKeepFromKey = 3f;
    
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
        _distanceToTarget = hit.point - transform.position ;
        // if player is dead, do nothing
        if(!_statistics.IsAlive) return;
        
        // if player is in combat, combat
        if (InteractWithCombat()){
            return;
        }
        
        // if target is interactable, interact
        
        // if click on the ground, move to cursor
        MoveToCursor();
        
        
        // if (_interactionRange > _distanceToTarget){
        //     _navmeshMover.StopMoving();
        // }
        
        if (_navmeshMover._navMeshAgent.remainingDistance < _navmeshMover._navMeshAgent.stoppingDistance){
            _navmeshMover.StopMoving();
            ChangeAnimationState(PLAYER_WALK);
        }
    }

    bool InteractWithCombat(){
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        foreach (RaycastHit hit in hits){
            TakeDamage enemy = hit.transform.GetComponent<TakeDamage>();
            if (enemy == null) continue;
            if (Input.GetMouseButton(0)){
                TryToAttackEnemy(enemy); // can be moved to player combat script
            }
            return true;
        }
        return false;
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
                    
                    if (_navmeshMover.pathFound){
                        StartCoroutine(ChangeCursorTemporary(validClickTexture,1f));
                        ChangeAnimationState(PLAYER_RUN);
                    }
                    else{
                        StartCoroutine(ChangeCursorTemporary(invalidClickTexture,1f));
                        ChangeAnimationState(PLAYER_WALK);
                    }
                }

                // else if (hit.transform.tag != "Ground" ){
                //     PlayMoveFeedback(1f);
                //     
                //     ChangeAnimationState(PLAYER_WALK);
                // }

                //Hits something
                else if(hit.transform.tag == "Interactable"){
                    PlayMoveFeedback(1f);
                    Vector3 newDestination = hit.point - _distanceToTarget.normalized * 1;
                    //_navmeshMover.Mover(hit.point - _distanceToTarget.normalized * 1);
                    //ChangeAnimationState(PLAYER_WALK);
                    if (_distanceToTarget.magnitude > distanceToKeepFromKey){
                        _navmeshMover.Mover(newDestination);
                        if(_navmeshMover.pathFound)
                            ChangeAnimationState(PLAYER_RUN);
                        StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
                    }
                }
                
                // else if(hit.transform.tag == "Enemy"){
                //     // if hit is enemy
                //     AttackEnemy();
                // }
                //Debug.Log(hit.transform.tag);
            }
            else{
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

    void TryToAttackEnemy(TakeDamage target){
        int enemyHealth = target.GetComponent<Statistics>().currentHP;

        // 1. if enemy is not alive, do nothing
        if (enemyHealth <= 0){
            Debug.Log("Enemy is dead");
            return;
        }

        bool isInAttackRange = Vector3.Distance(transform.position, target.transform.position) < 2f; // just for debug whether player is in attack range
        
        // 2. if enemy is alive and player is in attack range, attack
        if (enemyHealth > 0 && isInAttackRange){
            GetComponent<DealDamage>().Attack(5, target.gameObject);
            Debug.Log("Attacking");
        }

        // 3. if enemy is alive and there is no valid path, do nothing
        if (enemyHealth > 0 && !_navmeshMover.pathFound){
            Debug.Log("No valid path to the enemy.");
            return;
        }

        // 4. if enemy is alive and player is not in attack range and there is valid path, go to the enemy
        if (enemyHealth > 0 && !isInAttackRange){
            _navmeshMover.Mover(target.transform.position);
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
        Cursor.SetCursor(texture2D, Vector2.zero,CursorMode.ForceSoftware);
        yield return new WaitForSeconds(variable) ;
        Cursor.SetCursor(standardCursorTexture, Vector2.zero,CursorMode.ForceSoftware);
    }
}
