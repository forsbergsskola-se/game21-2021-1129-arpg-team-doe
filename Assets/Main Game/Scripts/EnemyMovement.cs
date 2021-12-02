using UnityEngine;

public class EnemyMovement : MonoBehaviour{
   
   [SerializeField] float attackRange = 5f;
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;
   
   TargetDetection _targetDetection;
   Player _player;
   Movement _movement;
   Fighter _fighter;
   Transform _desiredTarget;
   Transform _target;
   Transform _patrolTarget; 
   
   float distanceToPlayer;
   float distanceToTarget; 
   Vector3 savedPosition;
   bool activeSavedPosition; 
   bool needsToWalkBack; 

   void Start(){
      _player = FindObjectOfType<Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<TargetDetection>();
      _movement = GetComponent<Movement>();
      _fighter = GetComponent<Fighter>();
      _desiredTarget = _player.transform;
   }

   void Update(){
      //Sets target if detected and is not walkingback
      if (_targetDetection.TargetIsDetected(this.transform.position, _desiredTarget) && !needsToWalkBack){
         _target = _desiredTarget;
      }
      
      if (_target != null && !needsToWalkBack){
         
         //Calculates distance to target
         distanceToTarget = _targetDetection.DistanceToTarget(this.transform.position, _target);
         
         if (_target != _patrolTarget){ //This if check is meant for possibly future implementation of AI Patrolling
            if (!activeSavedPosition){
               SavePosition();
            }
                     
            if (distanceToTarget > attackRange && !needsToWalkBack)
            { 
               // stop attacking ??(Bool/State ATTACKING = False)??
               //_fighter.StopAttack(_target);
               _movement.Mover(_target.position);
            }
            
            //Checks if we're outside of the maxFollowRange
            if(_targetDetection.DistanceToTarget(savedPosition,transform) >= maxFollowRange){
               ForgetTarget();
            }
            
            if (distanceToTarget < attackRange){
               _movement.StopMoving();
               _fighter.Attack(_target);
               //attack target  ??(Bool/state ATTACKING = True)??.
            }
         }
      }
      
      if (_target == null){
         //Walk Back
         if (needsToWalkBack){
            _movement.Mover(savedPosition);
         }

         //Checks if this unit is close enough to saved position and already has an active saved position 
         if (_targetDetection.DistanceToTarget(savedPosition, transform ) < closeEnoughToSavedPosition && activeSavedPosition){
            SetIdle();
         }
         
      }
      
      if (_target == _patrolTarget){
         //AI Path walking (Just walking around)
      }

   }

   void SetIdle(){
      activeSavedPosition = false;
      needsToWalkBack = false;
   }

   void ForgetTarget(){
      _target = null;
      needsToWalkBack = true;
      //break;
   }

   void SavePosition(){
      savedPosition = this.transform.position;
      activeSavedPosition = true;
      Debug.Log(savedPosition);
   }
}
