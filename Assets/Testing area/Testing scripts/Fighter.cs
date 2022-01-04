using CustomLogs;
using UnityEngine;
using Random = System.Random;
using AnimatorChanger;
using FMOD.Studio;

public class Fighter : MonoBehaviour, IInteractSound{
    [SerializeField] float critDamageMultiplier = 1.5f; // for debug
    [SerializeField] DamageType wepDamageType; //TODO: Use actual weapon damage type here, just for debug for now
    
    Statistics _statistics;
    Health _combatTarget;
    Movement _movement;
    Random _random;
    AnimationController _animationController;
    Rigidbody _rigidbody;
    //FMOD
    EventInstance _critAttackInstance;
    public FMODUnity.EventReference critReference;
    EventInstance _attackInstance;
    public FMODUnity.EventReference attackReference;
    public FMODUnity.EventReference IdleReference;
    EventInstance idleInstance;
    
    int _damage;
    bool _isPlayer;
    float _attackRange;
    float _distance;
    float _timeSinceLastAttack = Mathf.Infinity;

    const string RUN = "Run";
    const string ATTACK = "Attack";
    const string IDLE = "Idle";

    void Start(){
        _statistics = GetComponent<Statistics>();
        _rigidbody = GetComponent<Rigidbody>();
        _attackRange = _statistics.AttackRange;
        _random = new Random();
        _movement = GetComponent<Movement>();
        _animationController = GetComponentInChildren<AnimationController>();
        if (gameObject.CompareTag("Player")) {
            _isPlayer = true;
        }

        if (gameObject.CompareTag("Player")){
            _critAttackInstance = FMODUnity.RuntimeManager.CreateInstance(critReference);
        }
        _attackInstance = FMODUnity.RuntimeManager.CreateInstance(attackReference);
        idleInstance = FMODUnity.RuntimeManager.CreateInstance(IdleReference);
    }

    void Update(){
        _timeSinceLastAttack += Time.deltaTime;
        if (_combatTarget == null && _rigidbody.velocity.magnitude == 0){
            Debug.Log("im idleing");
            idleInstance.start();
        }   
        if (_combatTarget == null){
            return;
        }
        if (!_combatTarget.GetComponent<Health>().IsAlive || IsClickOnItself()){
            _combatTarget = null;
            _animationController.ChangeAnimationState(IDLE);
            idleInstance.stop(STOP_MODE.IMMEDIATE);
            return;
        }
        if (!IsInAttackRange()){
            idleInstance.stop(STOP_MODE.IMMEDIATE);
            _movement.Mover(_combatTarget.transform.position, 1f);
            if (_animationController != null)
                _animationController.ChangeAnimationState(RUN);
        }
        else{
            idleInstance.stop(STOP_MODE.IMMEDIATE);
            _movement.StopMoving();
            Attack(_combatTarget.gameObject);
        }
    }

    public void GetAttackTarget(GameObject target){
        _combatTarget = target.GetComponent<Health>();
    }

    public void CancelAttack(){
        _combatTarget = null;
    }

    void Attack(GameObject target){
        
        LookAtTarget();
        if (_timeSinceLastAttack > 1f / _statistics.AttackSpeed){
            this.Log("I am doing damage");
            // TODO: trigger attack animation
            PlayAttackSound();
            //if(_animationController != null)
            _animationController.ChangeAnimationState(ATTACK);
            _damage = _statistics.AttackDamage;
            //Weapon dmg type vs enemy stat res type
            bool isCrit = false;
            if (_random.NextDouble() < _statistics.CritChance){
                _damage = Mathf.RoundToInt(_statistics.AttackDamage * critDamageMultiplier);
                isCrit = true;
                PlayCritSound(); 
            }
            target.GetComponent<IDamageReceiver>()?.ReceiveDamage(_damage, isCrit, _isPlayer, wepDamageType/*weapon.damageType */);
            //TODO: We need an actual weapon to get the damage type of that weapon
            _timeSinceLastAttack = 0f;
        }
    }

    void LookAtTarget(){
        Transform lookAtTransform = _combatTarget.transform;
        Vector3 lookAtPosition = lookAtTransform.transform.position;
        lookAtTransform.position = new Vector3(lookAtPosition.x, transform.position.y, lookAtPosition.z);
        transform.LookAt(lookAtTransform);
    }

    bool IsInAttackRange(){
        return Vector3.Distance(transform.position, _combatTarget.transform.position) < _attackRange;
    }
    bool IsClickOnItself(){
        return _combatTarget.transform.gameObject == gameObject;
    }
    public void PlayCritSound(){
        _critAttackInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _critAttackInstance.start();  
        }
    }
    public void PlayAttackSound(){
        _attackInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _attackInstance.start();  
        }
    }
}
