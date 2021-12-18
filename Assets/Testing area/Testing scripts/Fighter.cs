using CustomLogs;
using UnityEngine;
using Random = System.Random;
using AnimatorChanger;
using FMOD.Studio;

public class Fighter : MonoBehaviour, IInteractSound{
    [SerializeField] float critDamageMultiplier = 1.5f; // for debug

    Statistics _statistics;
    Health _combatTarget;
    Movement _movement;
    Random _random;
    AnimationController _animationController;
    EventInstance _critAttackInstance;
    public FMODUnity.EventReference critReference;
    EventInstance _attackInstance;
    public FMODUnity.EventReference attackReference;

    bool isPlayer;

    float _attackRange;
    float _distance;
    int _damage;
    float _timeSinceLastAttack = Mathf.Infinity;

    const string RUN = "Run";
    const string ATTACK = "Attack";

    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        _random = new Random();
        _movement = GetComponent<Movement>();
        _animationController = GetComponentInChildren<AnimationController>();
        if (this.gameObject.tag == "Player") {
            isPlayer = true;
        }

        if (gameObject.tag == "Player"){
            _critAttackInstance = FMODUnity.RuntimeManager.CreateInstance(critReference);
        }
        _attackInstance = FMODUnity.RuntimeManager.CreateInstance(attackReference);
    }

    void Update(){
        _timeSinceLastAttack += Time.deltaTime;
        if (_combatTarget == null){
            return;
        }
        if (!_combatTarget.GetComponent<Health>().IsAlive || IsClickOnItself()){
            _combatTarget = null;
            return;
        }
        if (!IsInAttackRange()){
            _movement.Mover(_combatTarget.transform.position);
            if (_animationController != null)
                _animationController.ChangeAnimationState(RUN);
        }
        else{
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
            // TODO: trigger attack animation and sound here
            PlayAttackSound();
            //if(_animationController != null)
            _animationController.ChangeAnimationState(ATTACK);
            _damage = _statistics.AttackDamage;
            bool isCrit = false;
            if (_random.NextDouble() < _statistics.CritChance){
                _damage = Mathf.RoundToInt(_statistics.AttackDamage * critDamageMultiplier);
                isCrit = true;
                PlayCritSound(); 
            }
            target.GetComponent<IDamageReceiver>()?.ReceiveDamage(_damage, isCrit, isPlayer);
            //this.LogDealDamage(_damage, isCrit, target,_combatTarget.CurrentHP);
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
