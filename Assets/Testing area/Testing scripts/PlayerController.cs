using System.Collections;
using CustomLogs;
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
    [SerializeField] int playerDefeatedThreshold = 20;
    [SerializeField] int playerRegenerateThreshold = 80;

    public InventoryObject inventory;
    internal bool playerIsDefeated;
    FMOD.Studio.EventInstance _moveInstance;
    Movement _movement;
    Statistics _statistics;
    Health _health;

    Animator _animator;
    RaycastHit _hit;
    string _currentState;
    float _interactionRange;
    bool _hasPlayedSound;
    bool _hasWaitedForTime;

    const string PLAYER_RUN = "playerRun";
    const string PLAYER_WALK = "playerWalk";

    void Start(){
        _movement = GetComponent<Movement>();
        _statistics = GetComponent<Statistics>();
        _interactionRange = _statistics.InteractRange;
        _animator = GetComponentInChildren<Animator>();
        _moveInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Move");
        _health = GetComponent<Health>();
    }

    void Update(){
        if (!_health.IsAlive){
            return;
        }
        this.LogHealth(_health.CurrentHP);
        if (GetPlayerIsDefeated()){
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

        if (_movement._navMeshAgent.remainingDistance < _movement._navMeshAgent.stoppingDistance){
            _movement.StopMoving();
            ChangeAnimationState(PLAYER_WALK);
        }
    }

    bool GetPlayerIsDefeated(){
        if (_health.CurrentHP <= playerDefeatedThreshold){
            playerIsDefeated = true;
        }
        if (_health.CurrentHP >= playerRegenerateThreshold){
            playerIsDefeated = false;
        }
        if (playerIsDefeated){
            _movement.enabled = false;
            PlayerRegeneration();
        }
        else{
            _movement.enabled = true;
        }
        return playerIsDefeated;
    }

    void PlayerRegeneration(){
        Debug.Log(this.name + " is defeated.");
        _health.UpdateHealth(-1);
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
            GameObject enemy = hit.transform.GetComponent<Health>()?.gameObject;
            if (enemy == null) continue;
            if (Input.GetMouseButton(0)){
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
            bool hasHit = Physics.Raycast(ray, out _hit);
            if (hasHit){
                if (_hit.transform.CompareTag("Ground")){
                    PlayMoveFeedback(0f);
                    //_moveInstance.release();
                    _movement.Mover(_hit.point);
                    if (_movement.pathFound){
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
                _movement.StopMoving();
                PlayMoveFeedback(1f);
                StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
                ChangeAnimationState(PLAYER_WALK);
            }
        }
        else if (Input.GetMouseButtonUp(0)){
            _moveInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _moveInstance.release();
            _hasPlayedSound = false;
        }
    }

    void MoveToInteractable(GameObject target, Vector3 destination){
        PlayMoveFeedback(1f);
        //ChangeAnimationState(PLAYER_WALK);
        bool isCloseEnoughToTarget = GetIsInRange(target.transform, _interactionRange);
        if(!isCloseEnoughToTarget){
            _movement.Mover(destination);
            if (_movement.pathFound)
                ChangeAnimationState(PLAYER_RUN);
            StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
        }
    }

    bool GetIsInRange(Transform target, float range){
        return Vector3.Distance(transform.position, target.position) < range;
    }

    static Ray GetMouseRay(){
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    void PlayMoveFeedback(float parameter){
        if (_hasPlayedSound == false){
            _moveInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Move");
            _moveInstance.setParameterByName("MoveFeedback", parameter);
            _moveInstance.start();
            _hasPlayedSound = true;
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
