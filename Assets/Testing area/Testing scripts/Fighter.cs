using UnityEngine;
using Random = System.Random;

public interface IDamageReceiver{
    void ReceiveDamage(int damage, bool isCrit);
}

public class Fighter : MonoBehaviour{
    [SerializeField] float critDamageMultiplier = 1.5f; // for debug
    [SerializeField] float critChance = 0.5f; // for debug
    [SerializeField] int weaponDamage = 5; // for debug, which will be calculated in statistics

    Statistics _statistics;
    TakeDamage _combatTarget;
    Movement _movement;
    Random _random;

    float _attackRange;
    float _attackSpeed;
    float _distance;
    bool isRanged;
    int _damage;
    float _timeSinceLastAttack = Mathf.Infinity;

    void Start(){
        _statistics = GetComponent<Statistics>();
        _attackRange = _statistics.AttackRange;
        _attackSpeed = _statistics.AttackSpeed;
        _random = new Random();
        //InvokeRepeating(nameof(Attack),0, 1f/ _attackSpeed);
        _movement = GetComponent<Movement>();
    }

    void Update(){
        _timeSinceLastAttack += Time.deltaTime;
        if (_combatTarget == null){
            return;
        }

        if (!_combatTarget.GetComponent<Statistics>().IsAlive){
            Debug.Log(_combatTarget.name + " is defeated.");
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
        Transform lookAtTransform = _combatTarget.transform;
        Vector3 lookAtPosition = lookAtTransform.transform.position;
        lookAtTransform.position = new Vector3(lookAtPosition.x,transform.position.y, lookAtPosition.z);
        transform.LookAt(lookAtTransform);

        if (_timeSinceLastAttack > 1f / _attackSpeed){
            // TODO: trigger attack animation and sound here
            _damage = weaponDamage;
            bool isCrit = false;
            if (_random.NextDouble() < critChance){
                _damage = Mathf.RoundToInt(weaponDamage * critDamageMultiplier);
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
        _combatTarget = target.GetComponent<TakeDamage>();
    }

    bool GetIsInRange(){
        return Vector3.Distance(transform.position, _combatTarget.transform.position) < _attackRange;
    }
}
