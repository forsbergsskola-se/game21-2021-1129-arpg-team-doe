using System.Collections;
using CustomLogs;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealthListener
{
   [SerializeField] float timeToVanish = 5f;
   CapsuleCollider _capsuleCollider;
   Fighter _fighter;
   EnemyController _enemyMovement;
   DropTest _dropTest;
   bool _hasDied;

   void Awake(){
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _fighter = GetComponent<Fighter>();
      _enemyMovement = GetComponent<EnemyController>();
      _dropTest = GetComponent<DropTest>();
   }
   void Die(bool isAlive){
      if (!isAlive && !_hasDied){
         _hasDied = true;
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
   public void HealthChanged(int currentHealth, int maxHealth, int damage, bool isCrit, bool isAlive){
      Die(isAlive);
   }
}
