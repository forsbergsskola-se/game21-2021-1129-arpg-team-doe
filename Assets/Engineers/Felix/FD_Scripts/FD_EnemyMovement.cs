using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float aggroRange = 30f;
   [SerializeField] float attackRange = 5f;
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;

   
   
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   FD_Movement _movement;
   Transform _desiredTarget;
   Transform _target;
   Transform _patrolTarget; //New Transform
   
   float distanceToPlayer;
   float distanceToTarget; //New float
   Vector3 savedPosition;

   
   bool activeSavedPosition; //New Bool
   bool needsToWalkBack; //New Bool
 
   bool isOutsideOfCombatRange;
   bool isWalkingback;
   bool isPursuing;
   bool targetIsDetected;
   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      _movement = GetComponent<FD_Movement>();
      _desiredTarget = _player.transform;

   }

   void Update(){

      //1. 
      //2 if (_targetDetection.TargetIsDetected(transform.position, _desiredTarget)
      //   - _target = _player;
      //3. distanceToTarget = _targetDetection.DistanceToTarget(transform.position, _target.transform);
      //
      //4. if (_target != null && _target != _patrolTarget)
      //   - if (!activeSavedPosition)
      //      - savedPosition = transform.position;
      //      - activeSavedPosition = true;    
      //   - if (distance to this scripts transform from the saved location is MRE than max follow range if(_targetDetection.DistanceToTarget(savedPosition,transform) >= maxFollowRange))
      //      - _target = null
      //      - needsToWalkBack = true;
      //      - break;
      //   - if (the distance to target is MORE than the attack range if (distanceToTarget > attackRange))
      //      - stop attacking ??(Bool/State ATTACKING = False)??
      //      - walk towards target(_movement.Mover(_target.transform.position)).
      //   - if (distance to target is less than attack range (((AND if target.(Bool isAlive))) (distanceToTarget < attackRange))
      //      - stop moving (_movement.StopMoving)
      //      - attack target  ??(Bool/state ATTACKING = True)??.
      //
      //5. if (_target == null)
      //   - if (needsToWalkBack)
      //      - Walk towards saved position (_movement.Mover(savedPosition))
      //      - needsToWalkBack = false;
      //   - if (transform.position == savedPosition && activeSavedPosition)
      //      - activeSavedPosition = false;
      //
      //6. if (_target == _patrolTarget)
      //      - AI Path walking (Just walking around)
      //
      //7.
      
      
      
       //2
      if (_targetDetection.TargetIsDetected(this.transform.position, _desiredTarget) && !needsToWalkBack){
         _target = _desiredTarget;
      }

      //4
      if (_target != null && !needsToWalkBack){
         
         distanceToTarget = _targetDetection.DistanceToTarget(this.transform.position, _target.transform);
         
         if (_target != _patrolTarget){ //This if check is meant for possibly future implementation of AI Patrolling
            if (!activeSavedPosition){
               savedPosition = this.transform.position;
               activeSavedPosition = true;    
               Debug.Log(savedPosition);
            }
                     
            if (distanceToTarget > attackRange && !needsToWalkBack)
            { 
               // stop attacking ??(Bool/State ATTACKING = False)??
               _movement.Mover(_target.transform.position);
            }
                                 
            if(_targetDetection.DistanceToTarget(savedPosition,transform) >= maxFollowRange)
            {
               _target = null;
               needsToWalkBack = true;
               //break;
            }
            
            if (distanceToTarget < attackRange){
               _movement.StopMoving();
               //attack target  ??(Bool/state ATTACKING = True)??.
            }
         }
         
      }
      
      //5
      if (_target == null){
         //Walk Back
         if (needsToWalkBack){
            _movement.Mover(savedPosition);
            
         }

         //Remove Saved Position
         if (_targetDetection.DistanceToTarget(savedPosition, transform ) < closeEnoughToSavedPosition && activeSavedPosition){
            activeSavedPosition = false;
            needsToWalkBack = false;
         }
         
      }
      
      //6
      if (_target == _patrolTarget){
         //AI Path walking (Just walking around)
      }
      
            
      
      
      
      //
      //
      //
      //  ResetBools();
      // //TODO: Saved position does not work the way we want it to.
      // if (UpdateSavePosition()){
      //    savedPosition = transform.position;
      //    Debug.Log(savedPosition);
      //    Debug.Log(UpdateSavePosition());
      // }
      //
      //
      //
      // DetectTarget();
      //
      // //If player is out of pursuit range and is not already walking back, walk back.
      // // if (isOutsideOfCombatRange && !isWalkingback){
      // //    GoBackToOriginalPosition();
      // //    return;
      // // }
      //
      // //Checks if the enemy is outside of the pursuit range (Returns true or false)
      //
      // //Calculates distance from enemy position to player
      // distanceToPlayer = _targetDetection.DistanceToTarget(transform.position, _player.transform);
      //
      // if (OutsideOfMaxAttackRange()){
      //    GoBackToOriginalPosition();
      //    return;
      // }
      // if (CheckEnemyIsInAttackRange()){
      //    
      //    if (_targetDetection.TargetIsVisible(transform.position, _player.transform, attackRange)){
      //       //Attack
      //       _movement.StopMoving();
      //       Debug.Log("I AM ATTACKING" + _player.name);
      //       return;
      //    }
      //    
      // }
      //
      // if (targetIsDetected){
      //    PursuitTarget();
      // }
      // if (!targetIsDetected && !isWalkingback && _targetDetection.DistanceToTarget(savedPosition, transform) > 6 ){
      //    GoBackToOriginalPosition();
      // }
      // // if (distanceToPlayer > attackRange){
      // //    //If player is not outside of range, pursuit player.
      // //    if (!isOutsideOfCombatRange && targetIsDetected){
      // //       PursuitTarget();
      // //    }
      // // }
      //
      

   }


   void ResetBools(){
      if (isWalkingback && _targetDetection.DistanceToTarget(savedPosition, transform) < 6){
         isWalkingback = false;
         targetIsDetected = false;
         isPursuing = false;
         Debug.Log("Resetting Bools");
      }
   }

   bool CanResetWalkingback(){
      return (isWalkingback && _targetDetection.DistanceToTarget(savedPosition, transform) < 0.001);
   }

   bool OutsideOfMaxAttackRange(){
      return isOutsideOfCombatRange = _targetDetection.DistanceToTarget(savedPosition, transform) > aggroRange;
   }

   bool CheckEnemyIsInAttackRange(){
      if (distanceToPlayer < attackRange){
         return true;
      }
      return false;
   }
   
   

   bool UpdateSavePosition(){
      if (!isPursuing && !isWalkingback){
         return true;
      }
      return false;
   }
   

   void DetectTarget(){
      // Enemy stands for a while with tranform.postion and then go back, can be changed to savedPosition to go back immediately
      if (_targetDetection.TargetIsDetected(savedPosition, _player.transform)){
         targetIsDetected = true;
         if (!isPursuing && !isWalkingback){
            savedPosition = transform.position;
         }
         isOutsideOfCombatRange = false;
      }
      
   }

   void PursuitTarget(){
      isPursuing = true;
      isWalkingback = false;
      _movement.Mover(_player.transform.position);
   }

   public void GoBackToOriginalPosition(){
      targetIsDetected = false;
      _movement.Mover(savedPosition);
      isWalkingback = true;
      isPursuing = false;
      
   }

   // bool CanGoBackToOriginalPosition(){
   //    
   // }
}
