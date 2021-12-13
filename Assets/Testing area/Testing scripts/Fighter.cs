using CustomLogs;
using UnityEngine;
using Random = System.Random;

public interface IDamageReceiver{
    void ReceiveDamage(int damage, bool isCrit);
}

public class Fighter : MonoBehaviour{
    [SerializeField] float critDamageMultiplier = 1.5f; // for debug

    Statistics _statistics;
    Health _combatTarget;
    Movement _movement;
    Random _random;
    Animator _animator;

    float _attackRange;
    float _distance;
    int _damage;
    float _timeSinceLastAttack = Mathf.Infinity;
    string _currentState;
    
    const string RUN = "Run";
    const string ATTACK = "Punch";
    const string SCREAM = "Scream";

    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        _random = new Random();
        _movement = GetComponent<Movement>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update(){
        _timeSinceLastAttack += Time.deltaTime;
        if (_combatTarget == null){
            ChangeAnimationState(SCREAM);
            return;
        }
        if (!_combatTarget.GetComponent<Health>().IsAlive || IsClickOnItself()){
            _combatTarget = null;
            ChangeAnimationState(SCREAM);
            return;
        }
        if (!IsInAttackRange()){
            _movement.Mover(_combatTarget.transform.position);
            ChangeAnimationState(RUN);
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
            ChangeAnimationState(ATTACK);
            _damage = _statistics.AttackDamage;
            bool isCrit = false;
            if (_random.NextDouble() < _statistics.CritChance){
                _damage = Mathf.RoundToInt(_statistics.AttackDamage * critDamageMultiplier);
                isCrit = true;
            }
            target.GetComponent<IDamageReceiver>()?.ReceiveDamage(_damage, isCrit);
            this.LogDealDamage(_damage, isCrit, target,_combatTarget.CurrentHP);
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
    
    void ChangeAnimationState(string newState){
        if (_currentState == newState) return;
        _animator.Play(newState);
        _currentState = newState;
    }
}
