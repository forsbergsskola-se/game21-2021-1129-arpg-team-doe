using UnityEngine;

public class Enemy : MonoBehaviour{

   Statistics _statistics;
   CapsuleCollider _capsuleCollider;
   Fighter _fighter;
   EnemyMovement _enemyMovement;
   bool isDead;

   void Start(){
      _statistics = GetComponent<Statistics>();
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _fighter = GetComponent<Fighter>();
      _enemyMovement = GetComponent<EnemyMovement>();
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
      isDead = true;
   }

   bool IsDead(){
      if (!_statistics.IsAlive && !isDead){ return true;}

      return false;
   }
}
