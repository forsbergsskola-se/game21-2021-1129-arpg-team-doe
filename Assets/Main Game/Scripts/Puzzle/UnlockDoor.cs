using UnityEngine;
using FMOD.Studio;

public interface IInteractSound{
    void PlaySound(float parameter){
    }
}

public class UnlockDoor : MonoBehaviour, Iinteractable,IInteractSound{
    
    [SerializeField]bool _locked = true;
    InventoryController _inventoryController;
    [Tooltip("If none, Masterkey is default")][SerializeField] ItemObject key;
    ItemObject masterKey;

    public FMODUnity.EventReference DoorReference;
    
    DoorConditions _doorConditions; 
    CursorOnDoor _cursorOnDoor;
    BoxCollider _collider;
    Animator _animator;
    EventInstance _doorInstance;
    
    bool _conditionCompleted;

    void Awake(){
        if (FindObjectOfType<DoorConditions>() != null){
            _doorConditions = FindObjectOfType<DoorConditions>();
        }
        _cursorOnDoor = GetComponent<CursorOnDoor>();
        _collider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
        _inventoryController = FindObjectOfType<InventoryController>();
        if (key == null)
        {
            key = masterKey;
        }
    }

    void Start(){
        _animator.enabled = false;
        _doorInstance = FMODUnity.RuntimeManager.CreateInstance(DoorReference);
    }

    void Update(){ //TODO: Expensive?
        if (!_locked){
            _conditionCompleted = true;
            _cursorOnDoor.openable = true;
        }
        else {
            if (_doorConditions != null){
                _conditionCompleted = false;
                _cursorOnDoor.openable = false;
                LockingMechanism();
            }
        }
    }

    public void UnlockDoorWithKey()
    {
        _locked = false;
    }
    
    public void Use(){
        if (_locked && !_inventoryController._inventoryItemList.Contains(key)){
            PlaySound(1f);
        }
        else if (!_locked || _inventoryController._inventoryItemList.Contains(key) ){
            OpenDoor();
        }
    }
    
    public void PlaySound(float parameter){
        _doorInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _doorInstance.setParameterByName("OpenLocked", parameter);
            _doorInstance.start();  
        }
    }

    void LockingMechanism(){
        _locked = !_conditionCompleted;
    }

    void OpenDoor(){
        PlaySound(0f);
        _collider.enabled = false;
        _animator.enabled = true;
    }
}
