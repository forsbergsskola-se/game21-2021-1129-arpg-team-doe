using UnityEngine;
using FMOD.Studio;

public interface IInteractSound{
    void PlaySound(float parameter){
        PLAYBACK_STATE playback_state;
    }
}

public class UnlockDoor : MonoBehaviour, Iinteractable,IInteractSound{
    
    [SerializeField]bool _locked = true;
    
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
    }

    void Start(){
        _animator.enabled = false;
        _doorInstance = FMODUnity.RuntimeManager.CreateInstance(DoorReference);
    }

    void Update(){
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
    
    public void Use(){
        if (_locked){
            PlaySound(1f);
        }
        else if (!_locked){
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
