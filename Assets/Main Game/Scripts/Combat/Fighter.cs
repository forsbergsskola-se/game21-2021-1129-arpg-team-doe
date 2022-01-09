using UnityEngine;
using Random = System.Random;
using FMOD.Studio;

public class Fighter : MonoBehaviour, IInteractSound{
    [SerializeField] float critDamageMultiplier = 1.5f; // for debug
    [SerializeField] float attackIntervalMultiplier = 1.5f;
    [SerializeField] DamageType wepDamageType; //TODO: Use actual weapon damage type here, just for debug for now
    
    public bool IsIdle{ get; private set; }
    public FMODUnity.EventReference critReference;
    public FMODUnity.EventReference attackReference;
    EventInstance _critAttackInstance;
    EventInstance _attackInstance;

    Statistics _statistics;
    Health _combatTarget;
    Movement _movement;
    Random _random;
    AnimationController _animationController;
    Rigidbody _rigidbody;
    
    int _damage;
    bool _isPlayer;
    float _attackRange;
    float _distance;
    float _timeSinceLastAttack;
    float _objectSpeed;

    const string RUN = "Run";
    const string ATTACK = "Attack";
    const string IDLE = "Idle";

    void Awake(){
        _statistics = GetComponent<Statistics>();
        _rigidbody = GetComponent<Rigidbody>();
        _random = new Random();
        _movement = GetComponent<Movement>();
        _animationController = GetComponentInChildren<AnimationController>();
    }

    void Start(){
        if (gameObject.CompareTag("Player")) {
            _isPlayer = true;
            _critAttackInstance = FMODUnity.RuntimeManager.CreateInstance(critReference);
        }
        _attackRange = _statistics.AttackRange;
        _attackInstance = FMODUnity.RuntimeManager.CreateInstance(attackReference);
    }

    void Update(){
        _objectSpeed = _rigidbody.velocity.magnitude;//objectspeed is always 0??
        _timeSinceLastAttack += Time.deltaTime; 
        if (_combatTarget == null && _objectSpeed < 0.1 || !_isPlayer && _objectSpeed < 0.001){
            IsIdle = true;
            //_movement.StopMovementSound();
        }
        
        if (_combatTarget == null){
            return;
        }
        
        if (!_movement.pathFound && _combatTarget.GetComponentInChildren<Enemy>()){ //TODO: why enemy 5&7 can't find path
            return;
        }
        
        if (!_combatTarget.GetComponent<Health>().IsAlive || IsClickOnItself()){
            _combatTarget = null;
            _animationController.ChangeAnimationState(IDLE);
            return;
        }
        
        if (!IsInAttackRange()){
            _movement.Mover(_combatTarget.transform.position, 1f);
            if (_animationController != null)
                _animationController.ChangeAnimationState(RUN);
        }
        
        else{
            _movement.StopMoving();
            Attack(_combatTarget.gameObject);
            _movement.StopMovementSound();
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
        if (_timeSinceLastAttack > attackIntervalMultiplier /_statistics.AttackSpeed){
            PlayAttackSound();
            _animationController.ChangeAnimationState(ATTACK);
            _damage = _statistics.AttackDamage;
            //Weapon dmg type vs enemy stat res type
            bool isCrit = false;
            if (_random.NextDouble() < _statistics.CritChance){
                _damage = Mathf.RoundToInt(_statistics.AttackDamage * critDamageMultiplier);
                isCrit = true;
                PlayCritSound();
                _timeSinceLastAttack = 0f;
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
    void PlayCritSound(){
        _critAttackInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _critAttackInstance.start();  
        }
    }
    void PlayAttackSound(){
        _attackInstance.getPlaybackState(out var playbackState);
        if (playbackState == PLAYBACK_STATE.STOPPED){
            _attackInstance.start();  
        }
    }
}
