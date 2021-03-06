using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_EnemyMovement : MonoBehaviour
{
   [SerializeField] float maxFollowRange = 30f;
   [SerializeField] float closeEnoughToSavedPosition = 3f;
   
   JB_TargetDetection _targetDetection;
   JB_PlayerMovement _playerMovement;
   JB_Movement _movement;
   JB_Fighter _fighter;
   JB_Statistics _statistics;
   
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
      _statistics = GetComponent<JB_Statistics>();
      _playerMovement = FindObjectOfType<JB_PlayerMovement>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<JB_TargetDetection>();
      _movement = GetComponent<JB_Movement>();
      _fighter = GetComponent<JB_Fighter>();
      _desiredTarget = _playerMovement.transform;
      attackRange = _statistics.AttackRange;
   }

   void Update(){ // very long update, might want to refactor
      //Sets target if detected and is not walkingback
     //Debug.Log(transform.name + "I have to go back?" + needsToWalkBack + _targetDetection.DistanceToTarget(savedPosition, transform));
      if (_targetDetection.TargetIsDetected(this.transform.position, _desiredTarget) && !needsToWalkBack){
         _target = _desiredTarget;
      }
      
      if (!needsToWalkBack){
         if (_target == null) return;
         
         //Calculates distance to target
         distanceToTarget = _targetDetection.DistanceToTarget(this.transform.position, _target);
         
         if (_target != _patrolTarget){
            //This if check is meant for possibly future implementation of AI Patrolling
            InteractWithCombat();
         }
      }
      
      if (_target == null){
         
         WalkBackAndSetIdle();
      }
      
      if (_target == _patrolTarget){
         //AI Path walking (Just walking around)
      }

   }

   void WalkBackAndSetIdle(){
      //Walk Back
      if (needsToWalkBack){
         _movement.Mover(savedPosition);
      }
      Debug.Log(_targetDetection.DistanceToTarget(savedPosition, transform));

      //Checks if this unit is close enough to saved position and already has an active saved position 
      if (_targetDetection.DistanceToTarget(savedPosition, transform) < closeEnoughToSavedPosition &&
          activeSavedPosition){
         SetIdle();
         Debug.Log("AM I SAVING");
      }
   }

   void InteractWithCombat(){
      if (!activeSavedPosition){
         SavePosition();
      }

      if (distanceToTarget > attackRange && !needsToWalkBack) // a lot of ifs, might be able to break it up?
      {
         StopAttackThenMoveToTarget();
      }

      //Checks if we're outside of the maxFollowRange
      if (_targetDetection.DistanceToTarget(savedPosition, transform) >= maxFollowRange){
         ForgetTarget();
      }

      if (distanceToTarget < attackRange){
         StopMovingThenAttackTarget();
      }
   }

   void StopMovingThenAttackTarget(){
      if (_target != null){
         _movement.StopMoving();
         transform.LookAt(_target);
         _fighter.Attack(_target);
      }
      
      //attack target  ??(Bool/state ATTACKING = True)??.
   }

   void StopAttackThenMoveToTarget(){
      // stop attacking ??(Bool/State ATTACKING = False)??
      //_fighter.StopAttack(_target);
      _movement.Mover(_target.position);
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
//      Debug.Log(savedPosition);
   }
}
