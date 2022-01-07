using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealthListener{
   [SerializeField] float timeToVanish = 5f;
   [SerializeField] EventReference deathSound;

   EventInstance _deathInstance;
   CapsuleCollider _capsuleCollider;
   Fighter _fighter;
   EnemyController _enemyMovement;
   AnimationController _animationController;
   DropTest _dropTest;
   bool _hasDied;
   const string DIE = "Die";

   void Awake(){
      _capsuleCollider = GetComponent<CapsuleCollider>();
      _fighter = GetComponent<Fighter>();
      _enemyMovement = GetComponent<EnemyController>();
      _dropTest = GetComponent<DropTest>();
      _animationController = GetComponentInChildren<AnimationController>();
      _deathInstance = RuntimeManager.CreateInstance(deathSound);
   }
   
   void Die(bool isAlive){
      if (!isAlive && !_hasDied){
         _deathInstance.start();
         _deathInstance.release();
         _hasDied = true;
         _capsuleCollider.enabled = false;
         _fighter.enabled = false;
         _enemyMovement.enabled = false;
         _dropTest.InstantiateItem();
         StartCoroutine(AnimatorDelay());
         StartCoroutine(CorpseVanish(timeToVanish));
      }
   }

   IEnumerator AnimatorDelay(){
      yield return new WaitForSeconds(0.5f);
      _animationController.ChangeAnimationState(DIE);
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
