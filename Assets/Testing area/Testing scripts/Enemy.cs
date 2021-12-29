using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   [SerializeField] float timeToVanish = 5f;
   CapsuleCollider _capsuleCollider;
   Fighter _fighter;
   EnemyMovement _enemyMovement;
   Health _health;
   bool _hasDied;

   void Start(){
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
      _hasDied = true;
      StartCoroutine(CorpseVanish(timeToVanish));
   }

   IEnumerator CorpseVanish(float time){
      yield return new WaitForSeconds(time);
      foreach (Transform child in transform){
         child.gameObject.SetActive(false);
      }
   }

   bool IsDead(){
      if (!_health.IsAlive && !_hasDied){ return true;}
      return false;
   }
}
