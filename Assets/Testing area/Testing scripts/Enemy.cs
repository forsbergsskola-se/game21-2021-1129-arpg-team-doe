using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour{

   Statistics _statistics;
   CapsuleCollider _capsuleCollider;
   Fighter _fighter;
   EnemyMovement _enemyMovement;
   Health _health;

   bool hasDied;

   void Start(){
      _statistics = GetComponent<Statistics>();
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _fighter = GetComponent<Fighter>();
      _enemyMovement = GetComponent<EnemyMovement>();
      _health = GetComponent<Health>();
   }

   void Update(){
      if (IsDead()) { Die(); } // debug
   }

   void Die(){
      // call death event (animations, sounds etc)
      Debug.Log(this.name + " Dead");
      _capsuleCollider.enabled = false;
      _fighter.enabled = false;
      _enemyMovement.enabled = false;
      GetComponent<NavMeshAgent>().enabled = false;
      hasDied = true;
   }

   bool IsDead(){
      if (!_health.IsAlive && !hasDied){ return true;}
      return false;
   }
}
