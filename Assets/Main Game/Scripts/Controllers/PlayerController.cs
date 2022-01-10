using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class PlayerController : MonoBehaviour{
    [SerializeField] Texture2D validClickTexture;
    [SerializeField] Texture2D invalidClickTexture;
    [SerializeField] Texture2D standardCursorTexture;
    [SerializeField] int defeatedThreshold = 40;
    [SerializeField] EventReference footstepSound;

    FMOD.Studio.EventInstance _moveInstance;
    FMOD.Studio.EventInstance _footstepInstance;
    Movement _movement;
    Statistics _statistics;
    Health _health;
    AnimationController _animationController;
    Fighter _fighter;
    InventoryController _inventoryController;
    RaycastHit _hit;
    Animator _animator;

    string _currentState;
    float _interactionRange;
    bool _hasPlayedSound;
    bool _playerIsDefeated;
    
    const string RUN = "Run";
    const string IDLE = "Idle";
    const string DIE = "Die";

    void Awake(){
        _movement = GetComponent<Movement>();
        _statistics = GetComponent<Statistics>();
        _moveInstance = RuntimeManager.CreateInstance("event:/Move");
        _health = GetComponent<Health>();
        _animationController = GetComponentInChildren<AnimationController>();
        _fighter = GetComponent<Fighter>();
        _inventoryController = FindObjectOfType<InventoryController>();
        _animator = GetComponentInChildren<Animator>();
        _footstepInstance = RuntimeManager.CreateInstance(footstepSound);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        _interactionRange = _statistics.InteractRange;
    }

    void Update(){
        if (!GetPlayerIsDefeated()){
            _movement.enabled = true;
            _fighter.enabled = true;
            StopCoroutine(_health.HealthRegeneration());
        }
        if (GetPlayerIsDefeated()){
            _movement.StopMoving();
            _movement.enabled = false; 
            _fighter.enabled = false;
            _animationController.ChangeAnimationState(DIE);
            if (!_health.isRegenerating){
                StartCoroutine(_health.HealthRegeneration());
            }
            return;
        }

        if (Input.GetMouseButton(0) && _inventoryController.clickOnUI){return;}
        if (_inventoryController.selectedItem != null){return;}

        if (Input.GetMouseButton(0)){
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if (InteractWithPickup(hits)){return;}
            if (InteractWithCombat(hits)){return;}
            if (InteractWithInteractable(hits)){return;} 
        }

        MoveToCursor();
    }

    bool GetPlayerIsDefeated(){
        if (_health.CurrentHP <= defeatedThreshold){
            _playerIsDefeated = true;
        }
        if (_health.CurrentHP >= _health.stopRegenerateThreshold){
            _playerIsDefeated = false;
        }
        return _playerIsDefeated;
    }

    bool InteractWithCombat(RaycastHit[] hits){
        foreach (RaycastHit hit in hits){
            GameObject enemy = hit.transform.GetComponent<Health>()?.gameObject;
            if (enemy == null) {continue;}
            _fighter.GetAttackTarget(enemy);
            _footstepInstance.stop(STOP_MODE.ALLOWFADEOUT);
            return true;
        }
        return false;
    }

    bool InteractWithInteractable(RaycastHit[] hits){
        foreach (RaycastHit hit in hits){
            GameObject interactableObject = hit.transform.GetComponent<InteractableObject>()?.gameObject;
            if (interactableObject == null) {continue;}
            StartCoroutine(GoToPositionThenInteract(hit));
            Vector3 positionCloseToTarget = hit.point - (hit.point - transform.position).normalized;
            MoveToInteractable(interactableObject, positionCloseToTarget);
            _animationController.ChangeAnimationState(RUN);
            PlaySound();
            return true;
        }
        return false;
    }
    
    bool InteractWithPickup(RaycastHit[] hits){
        foreach (RaycastHit hit in hits){
            GameObject pickUpGameObject = hit.transform.GetComponent<PickUpItem>()?.gameObject;
            if (pickUpGameObject == null) {continue;}
            if (Input.GetKey(KeyCode.LeftControl)){
                InteractWithInteractable(hits);
            }
            else{
                DragObject(pickUpGameObject);
            }
            return true;
        }
        return false;
    }

    void DragObject(GameObject pickUpGameObject){
        InventoryItem inventoryItem = pickUpGameObject.GetComponent<InventoryItem>();
        Destroy(pickUpGameObject);
        _inventoryController.selectedItem = _inventoryController.CreateItem(inventoryItem.itemObject.Id);
        _inventoryController.selectedItem.itemObject.PlayPickupSound();
    }

    void MoveToCursor(){
        if (Input.GetMouseButton(0)){
            Ray ray = GetMouseRay();
            bool hasHit = Physics.Raycast(ray, out _hit);
            if (hasHit){
                if (_hit.transform.CompareTag("Ground")){
                    PlayMoveFeedback(0f);
                    _movement.Mover(_hit.point, 1f);
                    if (_movement.pathFound){
                        _fighter.CancelAttack();
                        StartCoroutine(ChangeCursorTemporary(validClickTexture,1f));
                        _animationController.ChangeAnimationState(RUN);
                        PlaySound();
                    }
                    else{
                        StartCoroutine(ChangeCursorTemporary(invalidClickTexture,1f));
                    }
                }
                else if (_hit.transform.CompareTag("Wall")){
                    StartCoroutine(ChangeCursorTemporary(invalidClickTexture,1f));
                }
            }
            else{
                _movement.StopMoving();
                PlayMoveFeedback(1f);
                StartCoroutine(ChangeCursorTemporary(invalidClickTexture, 1f));
                _animationController.ChangeAnimationState(IDLE);
                _footstepInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        else if (Input.GetMouseButtonUp(0)){
            _moveInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _moveInstance.release();
            _hasPlayedSound = false;
        }
        
        if (_movement._navMeshAgent.remainingDistance < _movement._navMeshAgent.stoppingDistance){
            _movement.StopMoving();
            _animationController.ChangeAnimationState(IDLE);
            _footstepInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    void MoveToInteractable(GameObject target, Vector3 destination){
        PlayMoveFeedback(1f);
        bool isCloseEnoughToTarget = GetIsInRange(target.transform, _interactionRange);
        if(!isCloseEnoughToTarget){
            _movement.Mover(destination, 1f);
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
            _moveInstance = RuntimeManager.CreateInstance("event:/Move");
            _moveInstance.setParameterByName("MoveFeedback", parameter);
            _moveInstance.start();
            _hasPlayedSound = true;
        }
    }
    void PlaySound(){
        _footstepInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _footstepInstance.start();  
        }
    }

    IEnumerator GoToPositionThenInteract(RaycastHit hit){
        _movement.Mover(hit.point, 1f);
        while (GetIsInRange(hit.transform, _interactionRange) == false){
            yield return null;
        }
        // hit.transform.GetComponent<Iinteractable>()?.Use();
        foreach (var interactables in hit.transform.GetComponents<Iinteractable>())
        {
            interactables?.Use();
        }
    }

    IEnumerator ChangeCursorTemporary(Texture2D texture2D,float variable){
        Cursor.SetCursor(texture2D, Vector2.zero,CursorMode.ForceSoftware);
        yield return new WaitForSeconds(variable) ;
        Cursor.SetCursor(standardCursorTexture, Vector2.zero,CursorMode.ForceSoftware);
    }
}