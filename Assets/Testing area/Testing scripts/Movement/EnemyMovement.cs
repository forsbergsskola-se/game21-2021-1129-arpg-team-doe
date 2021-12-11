using UnityEngine;

public class EnemyMovement : MonoBehaviour
{ 
   [SerializeField] GameObject _healthBar;
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;
  
   TargetDetection _targetDetection;
   Movement _movement;
   Fighter _fighter;
   Health _health;
   
   GameObject _player;
   Transform _desiredTarget;
   Transform _target;
   Transform _patrolTarget;
   Vector3 _savedPosition;
   bool _activeSavedPosition; 
   bool _needsToWalkBack; 

   void Start(){
      _targetDetection = GetComponent<TargetDetection>();
      _movement = GetComponent<Movement>();
      _fighter = GetComponent<Fighter>();
      _health = GetComponent<Health>();
      _player = GameObject.FindWithTag("Player");
      _desiredTarget = _player.transform;
   }

   void Update(){ // very long update, might want to refactor
      //Sets target if detected and is not walking back
      if (_targetDetection.TargetIsDetected(transform.position, _desiredTarget) && !_needsToWalkBack){
         _target = _desiredTarget;
      }
      
      if (!_needsToWalkBack){
         if (_target == null) return;
         InteractWithCombat();
      }
      
      if (_target == null){
         WalkBackAndSetIdle();
         // Or do patrol behavior
      }
   }

   void WalkBackAndSetIdle(){
      if (_needsToWalkBack){
         _movement.Mover(_savedPosition);
      }
      //Debug.Log(_targetDetection.DistanceToTarget(savedPosition, transform));

      //Checks if this unit is close enough to saved position and already has an active saved position 
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) < closeEnoughToSavedPosition &&
          _activeSavedPosition){
         SetIdle();
      }
   }

   void InteractWithCombat(){

      if (!_activeSavedPosition){
         SavePosition();
      }
      if (!_health.IsAlive){
         return;
      }
      _fighter.GetAttackTarget(_target.gameObject);
      _healthBar.SetActive(true);

      //Checks if we're outside of the maxFollowRange
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) >= maxFollowRange){
         ForgetTarget();
      }
   }
   
   void SetIdle(){
      _activeSavedPosition = false;
      _needsToWalkBack = false;
   }

   void ForgetTarget(){
      _target = null;
      _needsToWalkBack = true;
      _healthBar.SetActive(false);
   }

   void SavePosition(){
      _savedPosition = this.transform.position;
      _activeSavedPosition = true;
   }
}
