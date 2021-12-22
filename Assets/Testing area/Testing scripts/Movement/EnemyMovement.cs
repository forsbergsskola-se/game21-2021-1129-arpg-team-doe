#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine;
using AnimatorChanger;
using FMOD.Studio;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] GameObject _healthBar;
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;

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

      _alertInstance = FMODUnity.RuntimeManager.CreateInstance(alertReference);

   }

   void Update(){ // very long update, might want to refactor
      if(!_health.IsAlive) return;
      //Sets target if detected and is not walking back
      _playerIsDetected = _targetDetection.TargetIsDetected(transform.position, _desiredTarget);
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) < closeEnoughToSavedPosition){
         _needsToWalkBack = false;
         if (!_isAttacking) 
            _animationController.ChangeAnimationState(IDLE);
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
      // if (_target == null){
      //    WalkBackAndSetIdle();
      //    // Or do patrol behavior
      // }
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
         _healthBar.SetActive(true);
      }
      else{
         WalkBackAndSetIdle();
      }
   }

   // void InteractWithCombat(){
   //    if (!_activeSavedPosition){
   //       SavePosition();
   //    }
   //    if (_target != null && isAttacking){
   //       _fighter.GetAttackTarget(_target.gameObject);
   //       _healthBar.SetActive(true);
   //    }
   //    //Checks if we're outside of the maxFollowRange
   //    if (_targetDetection.DistanceToTarget(_savedPosition, transform) >= maxFollowRange){
   //       ForgetTarget();
   //    }
   // }

   void WalkBackAndSetIdle(){
      ForgetTarget();
      _movement.Mover(_savedPosition);
      _fighter.CancelAttack();
      _animationController.ChangeAnimationState(RUN);

      //Checks if this unit is close enough to saved position and already has an active saved position
      if (_targetDetection.DistanceToTarget(_savedPosition, transform) < closeEnoughToSavedPosition &&
          _activeSavedPosition){
         SetIdle();
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
