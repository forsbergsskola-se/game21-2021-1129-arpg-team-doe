using System.Collections;
using CustomLogs;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHealthListener
{
   [SerializeField] float timeToVanish = 5f;
   CapsuleCollider _capsuleCollider;
   Fighter _fighter;
   EnemyMovement _enemyMovement;
   Health _health;
   DropTest _dropTest;
   bool _hasDied;

   void Start(){
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _fighter = GetComponent<Fighter>();
      _enemyMovement = GetComponent<EnemyMovement>();
      _health = GetComponent<Health>();
      _dropTest = GetComponent<DropTest>();
   }
   void Die(bool isAlive){
      if (!isAlive && !_hasDied)
      {
         _hasDied = true;
         this.Log("I am being called DEAD");
         // call death event (animations, sounds etc)
         _capsuleCollider.enabled = false;
         _fighter.enabled = false;
         _enemyMovement.enabled = false;
         
         _dropTest.InstantiateItem();
         StartCoroutine(CorpseVanish(timeToVanish));
      }
   }
   IEnumerator CorpseVanish(float time){
      yield return new WaitForSeconds(time);
      foreach (Transform child in transform){
         child.gameObject.SetActive(false);
      }
   }
   public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive)
   {
      Die(isAlive);
   }
}
