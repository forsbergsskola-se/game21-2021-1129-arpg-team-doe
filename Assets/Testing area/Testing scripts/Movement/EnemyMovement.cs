using UnityEngine;

public class EnemyMovement : MonoBehaviour
{ 
   [SerializeField] GameObject _healthBar;
   
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;
  
   TargetDetection _targetDetection;
   Movement _movement;
   Fighter _fighter;
   Statistics _statistics;
   Health _health;
   GameObject _player;

   Transform _desiredTarget;
   Transform _target;
   Transform _patrolTarget; 
   
   Vector3 savedPosition;
   
   float distanceToPlayer;
   float distanceToTarget;
   float attackRange;
   
   
   bool activeSavedPosition; 
   bool needsToWalkBack; 

   void Start(){
      _statistics = GetComponent<Statistics>();
      _targetDetection = GetComponent<TargetDetection>();
      _movement = GetComponent<Movement>();
      _fighter = GetComponent<Fighter>();
      _health = GetComponent<Health>();
      _player = GameObject.FindWithTag("Player");
      _desiredTarget = _player.transform;
      attackRange = _statistics.AttackRange;
   }

   void Update(){ // very long update, might want to refactor
      //Sets target if detected and is not walkingback
     //Debug.Log(transform.name + "I have to go back?" + needsToWalkBack + _targetDetection.DistanceToTarget(savedPosition, transform));
      if (_targetDetection.TargetIsDetected(transform.position, _desiredTarget) && !needsToWalkBack){
         _target = _desiredTarget;
      }
      
      if (!needsToWalkBack){
         if (_target == null) return;
         
         //Calculates distance to target
         distanceToTarget = _targetDetection.DistanceToTarget(transform.position, _target);
         InteractWithCombat();
      }
      
      if (_target == null){
         WalkBackAndSetIdle();
         // Or do patrol behavior
      }
   }

   void WalkBackAndSetIdle(){
      //Walk Back
      if (needsToWalkBack){
         _movement.Mover(savedPosition);
      }
      //Debug.Log(_targetDetection.DistanceToTarget(savedPosition, transform));

      //Checks if this unit is close enough to saved position and already has an active saved position 
      if (_targetDetection.DistanceToTarget(savedPosition, transform) < closeEnoughToSavedPosition &&
          activeSavedPosition){
         SetIdle();
         //Debug.Log("AM I SAVING");
      }
   }

   void InteractWithCombat(){

      if (!activeSavedPosition){
         SavePosition();
      }
      
      if(!_health.IsAlive) return;
      _fighter.GetAttackTarget(_target.gameObject);
      

      // if (distanceToTarget > attackRange && !needsToWalkBack) // a lot of ifs, might be able to break it up?
      // {
      //    StopAttackThenMoveToTarget();
      // }

      //Checks if we're outside of the maxFollowRange
      if (_targetDetection.DistanceToTarget(savedPosition, transform) >= maxFollowRange){
         ForgetTarget();
      }

      // if (distanceToTarget < attackRange){
      //    StopMovingThenAttackTarget();
      // }
   }

   void StopMovingThenAttackTarget(){
      if (_target != null){
         _movement.StopMoving();
         transform.LookAt(_target);
         //_fighter.Attack(_target.gameObject);
         _fighter.Attack(_player);
      }
      
      //attack target  ??(Bool/state ATTACKING = True)??.
   }

   void StopAttackThenMoveToTarget(){
      // stop attacking ??(Bool/State ATTACKING = False)??
      //_fighter.StopAttack(_target);
      _movement.Mover(_target.position);
      _healthBar.SetActive(true);
   }

   void SetIdle(){
      activeSavedPosition = false;
      needsToWalkBack = false;
   }

   void ForgetTarget(){
      _target = null;
      needsToWalkBack = true;
      _healthBar.SetActive(false);
      
      //break;
   }

   void SavePosition(){
      savedPosition = this.transform.position;
      activeSavedPosition = true;
//      Debug.Log(savedPosition);
   }
}
