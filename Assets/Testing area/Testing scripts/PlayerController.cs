using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMODUnity;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Texture2D validClickTexture;
    [SerializeField] Texture2D invalidClickTexture;
    [SerializeField] Texture2D standardCursorTexture;
    [SerializeField] float distanceToKeepFromKey = 3f;
    [SerializeField] float attackRange = 2f;

    public InventoryObject inventory;
    
    FMOD.Studio.EventInstance _moveInstance;
    Movement _navmeshMover;
    Statistics _statistics;

    Animator _animator;
    RaycastHit hit;

    string _currentState;
    float _interactionRange;
    bool hasPlayedSound;
    bool hasWaitedForTime;

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
        if (!_statistics.IsAlive){
            return;
        }
        
        if (InteractWithCombat()){
            return;
        }

        if (InteractWithInteractable()){
            return;
        }
        
        // if click on the ground, move to cursor
        MoveToCursor();

        if (_navmeshMover._navMeshAgent.remainingDistance < _navmeshMover._navMeshAgent.stoppingDistance){
            _navmeshMover.StopMoving();
            ChangeAnimationState(PLAYER_WALK);
        }
    }

    void OnTriggerEnter(Collider other){
        //Check if other has Item script
        if (other.GetComponent<Item>() != null){
            var item = other.GetComponent<Item>();
            //Adds item to inventory
            inventory.AddItem(item.item	,1);
            //Destroys the item from the world
            Destroy(other.gameObject);
        }
    }

    void OnApplicationQuit(){
        inventory.Container.Clear();
    }

    bool InteractWithCombat(){
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits){
            GameObject enemy = hit.transform.GetComponent<TakeDamage>()?.gameObject;
            if (enemy == null) continue;
            if (Input.GetMouseButton(0)){
                //TryToAttackEnemy(enemy);
                GetComponent<Fighter>().GetAttackTarget(enemy);
            }
            return true;
        }
        return false;
    }

    bool InteractWithInteractable(){
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits){
            GameObject interactableObject = hit.transform.GetComponent<InteractableObject>()?.gameObject;
            if (interactableObject == null) continue;
            if (Input.GetMouseButton(0)){
                Vector3 positionCloseToTarget = hit.point - (hit.point - transform.position).normalized * 1;
                MoveToInteractable(interactableObject, positionCloseToTarget); 
            }
            return true;
        }
        return false;
    }

    void MoveToCursor(){
        if (Input.GetMouseButton(0)){
            Ray ray = GetMouseRay();
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit){
                if (hit.transform.CompareTag("Ground")){
                    PlayMoveFeedback(0f);
                    //_moveInstance.release();
                    _navmeshMover.Mover(hit.point);
                    if (_navmeshMover.pathFound){
                        GetComponent<Fighter>().CancelAttack();
                        StartCoroutine(ChangeCursorTemporary(validClickTexture,1f));
                        ChangeAnimationState(PLAYER_RUN);
                    }
                    else{
                        StartCoroutine(ChangeCursorTemporary(invalidClickTexture,1f));
                        ChangeAnimationState(PLAYER_WALK);
                    }
                }
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

    void MoveToInteractable(GameObject target, Vector3 destination){
        PlayMoveFeedback(1f);
        //ChangeAnimationState(PLAYER_WALK);
        bool isCloseEnoughToTarget = GetIsInRange(target.transform, distanceToKeepFromKey);
        if(!isCloseEnoughToTarget){
            _navmeshMover.Mover(destination);
            if (_navmeshMover.pathFound)
                ChangeAnimationState(PLAYER_RUN);
            StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
        }
    }

    void TryToAttackEnemy(GameObject target){
        bool targetIsAlive = target.GetComponent<Statistics>().IsAlive;
        if (!targetIsAlive){
            return;
        }
        bool isInAttackRange = GetIsInRange(target.transform, attackRange);
        // 2. if player is in attack range, attack
        if (isInAttackRange){
            GetComponent<Fighter>().Attack(target.gameObject);
            Debug.Log("Attacking");
        }
        // 3. if there is no valid path, do nothing
        if (!_navmeshMover.pathFound){
            Debug.Log("No valid path to the enemy.");
            return;
        }
        // 4. if player is not in attack range and there is valid path, go to the enemy
        if (!isInAttackRange){
            _navmeshMover.Mover(target.transform.position);
        }
    }

    bool GetIsInRange(Transform target, float range){
        return Vector3.Distance(transform.position, target.position) < range;
    }
    
    static Ray GetMouseRay(){
        return Camera.main.ScreenPointToRay(Input.mousePosition);
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