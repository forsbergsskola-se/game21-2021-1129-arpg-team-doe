#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine;
using AnimatorChanger;
using FMOD.Studio;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] GameObject healthBar;
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;
   [SerializeField] PatrolPath patrolPath;
   [SerializeField] float waypointTolerance = 4f;
   [Range(0,1)]
   [SerializeField] float patrolSpeedFraction = 0.3f;
   [SerializeField] float dwellingTime = 6f;
   
   TargetDetection _targetDetection;
   Movement _movement;
   Fighter _fighter;
   Health _health;
   AnimationController _animationController;
   EventInstance _alertInstance;
   public FMODUnity.EventReference alertReference;
   
   GameObject _player;
   Transform _desiredTarget;
   Transform _target;
   Vector3 _savedPosition;
   float _dwellingTimer = Mathf.Infinity;
   int _currentWaypointIndex;
   bool _activeSavedPosition;
   bool _needsToWalkBack;

   bool _isAttacking;
   bool _playerIsDetected;
   string _currentState;
   bool _alerted = true;

   const string RUN = "Run";
   const string IDLE = "Idle";

   void Start(){
      _targetDetection = GetComponent<TargetDetection>();
      _movement = GetComponent<Movement>();
      _fighter = GetComponent<Fighter>();
      _health = GetComponent<Health>();
      _animationController = GetComponentInChildren<AnimationController>();
      _player = GameObject.FindWithTag("Player");
      _desiredTarget = _player.transform;
      _savedPosition = transform.position;

      _alertInstance = FMODUnity.RuntimeManager.CreateInstance(alertReference);

   }

   void Update(){ // very long update, might want to refactor
      if(!_health.IsAlive) return;
      //Sets target if detected and is not walking back
      _playerIsDetected = _targetDetection.TargetIsDetected(transform.position, _desiredTarget);
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) < closeEnoughToSavedPosition){
         _needsToWalkBack = false;
         if (!_isAttacking && patrolPath == null){
            _animationController.ChangeAnimationState(IDLE);
         }
      }
      if (!_playerIsDetected && patrolPath != null && !_needsToWalkBack){
         Patrol();
         _dwellingTimer += Time.deltaTime;
      }
      _isAttacking = _playerIsDetected && !_needsToWalkBack;
      if (_isAttacking){
            _target = _desiredTarget;
            InteractCombat(_target);
      }
      
      if (_playerIsDetected && _alerted){
         PlayAlertSound();
         _alerted = false;
      }
   }

   void InteractCombat(Transform target){
      if (!_activeSavedPosition){
         SavePosition();
      }
      if (!_target.GetComponent<Health>().IsAlive){
         WalkBackAndSetIdle();
         return;
      }
      // if (_target.GetComponent<PlayerController>().playerIsDefeated){
      //    WalkBackAndSetIdle();
      //    return;
      // }
      if (!_playerIsDetected && !_alerted){
         _alerted = true;
      }
      
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) < maxFollowRange){
         _fighter.GetAttackTarget(target.gameObject);
         _needsToWalkBack = false;
         healthBar.SetActive(true);
      }
      else{
         WalkBackAndSetIdle();
      }
   }

   void WalkBackAndSetIdle(){
      ForgetTarget();
      _movement.Mover(_savedPosition, 1f);
      _fighter.CancelAttack();
      _animationController.ChangeAnimationState(RUN);

      //Checks if this unit is close enough to saved position and already has an active saved position
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) < closeEnoughToSavedPosition &&
          _activeSavedPosition){
         SetIdle();
      }
   }

   void Patrol(){
      if (HasArrivedWaypoint()){
         _currentWaypointIndex = patrolPath.GetNextWaypointIndex(_currentWaypointIndex);
         _dwellingTimer = 0;
         _animationController.ChangeAnimationState(IDLE);
      }
      var nextWaypointPosition = GetCurrentWaypointPosition();
      if (_dwellingTimer > dwellingTime){
         _movement.Mover(nextWaypointPosition, patrolSpeedFraction);
         _animationController.ChangeAnimationState(RUN);
      }
   }
   
   bool HasArrivedWaypoint(){
      float distanceToWaypoint = _targetDetection.DistanceToTarget(GetCurrentWaypointPosition(), transform);
      return distanceToWaypoint < waypointTolerance;
   }

   Vector3 GetCurrentWaypointPosition(){
      return patrolPath.GetWaypointPosition(_currentWaypointIndex);
   }

   void SetIdle(){
      _activeSavedPosition = false;
      _needsToWalkBack = false;
   }

   void ForgetTarget(){
      _target = null;
      _needsToWalkBack = true;
      healthBar.SetActive(false);
   }

   void SavePosition(){
      _savedPosition = transform.position;
      _activeSavedPosition = true;
   }
   public void PlayAlertSound(){
      _alertInstance.getPlaybackState(out var playbackState);
      if (playbackState == PLAYBACK_STATE.STOPPED){
         _alertInstance.start();  
      }
   }

   void Alert(){
      if (_targetDetection.TargetIsDetected(transform.position,_target.transform)&& !_alerted){
         _alerted = true;
      }
   }


   #if UNITY_EDITOR
   void OnDrawGizmosSelected(){
      Handles.color = Color.magenta;
      Handles.DrawWireDisc(_savedPosition, transform.up,maxFollowRange);
      Handles.color = Color.white;
   }
   #endif
}
