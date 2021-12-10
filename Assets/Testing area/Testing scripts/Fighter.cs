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

    float _attackRange;
    float _distance;
    int _damage;
    float _timeSinceLastAttack = Mathf.Infinity;

    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        _random = new Random();
        _movement = GetComponent<Movement>();
    }

    void Update(){
        _timeSinceLastAttack += Time.deltaTime;
        if (_combatTarget == null){
            return;
        }

        if (!_combatTarget.GetComponent<Health>().IsAlive){
            _combatTarget = null;
            return;
        }

        if (!GetIsInRange()){
            _movement.Mover(_combatTarget.transform.position);
        }

        else{
            _movement.StopMoving();
            Attack(_combatTarget.gameObject);
        }
    }

    public void Attack(GameObject target){
        LookAtTarget();
        if (_timeSinceLastAttack > 1f / _statistics.AttackSpeed){
            // TODO: trigger attack animation and sound here
            _damage = _statistics.AttackDamage;
            bool isCrit = false;
            if (_random.NextDouble() < _statistics.CritChance){
                _damage = Mathf.RoundToInt(_statistics.AttackDamage * critDamageMultiplier);
                isCrit = true;
            }
            target.GetComponent<IDamageReceiver>()?.ReceiveDamage(_damage, isCrit);
            Debug.Log(transform.name + " is dealing " + _damage + " damage to " + _combatTarget.name);
            _timeSinceLastAttack = 0f;
        }
    }

    public void CancelAttack(){
        _combatTarget = null;
    }

    public void GetAttackTarget(GameObject target){
        _combatTarget = target.GetComponent<Health>();
    }

    void LookAtTarget(){
        Transform lookAtTransform = _combatTarget.transform;
        Vector3 lookAtPosition = lookAtTransform.transform.position;
        lookAtTransform.position = new Vector3(lookAtPosition.x, transform.position.y, lookAtPosition.z);
        transform.LookAt(lookAtTransform);
    }

    bool GetIsInRange(){
        return Vector3.Distance(transform.position, _combatTarget.transform.position) < _attackRange;
    }
}
