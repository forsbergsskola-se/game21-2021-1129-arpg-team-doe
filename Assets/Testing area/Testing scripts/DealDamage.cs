using UnityEngine;
using Random = System.Random;

public interface IDamageReceiver{
    void ReceiveDamage(int damage);
}

public class DealDamage : MonoBehaviour{
    [SerializeField] float critDamageMultiplier = 1.5f;
    [SerializeField] float critChance = 0.5f;
    [SerializeField] int weaponDamage = 5;

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
        if (_combatTarget == null) return;
        if (!_combatTarget.GetComponent<Statistics>().IsAlive) return;
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
        lookAtTransform.position = new Vector3(lookAtPosition.x,0f, lookAtPosition.z);
        transform.LookAt(lookAtTransform);
        
        if (_timeSinceLastAttack > 1f / _attackSpeed){
            _damage = weaponDamage;
            if (_random.NextDouble() < critChance){
                _damage = Mathf.RoundToInt(weaponDamage * critDamageMultiplier);
            }
            target.GetComponent<IDamageReceiver>()?.ReceiveDamage(_damage); // check!!!
            Debug.Log(transform.name + " is dealing " + _damage + " damage to " + _combatTarget.name);
            _timeSinceLastAttack = 0f;
        }
        
        //return damage; //Is this needed?
    }

    public void CancelAttack(){
        _combatTarget = null;
    }

    public void GetAttackTarget(GameObject target){
        _combatTarget = target.GetComponent<TakeDamage>();
    }

    // int DoMeleeDamage(int damage, GameObject target){
    //     return DamageManipulation(damage, target, _statistics.Strength);
    // }
    //
    // int DoRangedDamage(int damage, GameObject target){
    //     return DamageManipulation(damage, target, _statistics.Dexterity);
    // }

    int DamageManipulation(int damage, GameObject target, float modifier ){
        _distance = Vector3.Distance(transform.position, target.transform.position);
        if (_distance <= _attackRange){
            //Attack
            damage = Mathf.RoundToInt((modifier * 0.01f + 1) * damage);
        }
        return damage;
    }
    
    bool GetIsInRange(){
        return Vector3.Distance(transform.position, _combatTarget.transform.position) < _attackRange;
    }
}
