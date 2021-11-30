using System;
using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float pursuitRange = 30f;
   
   
   bool isOutOfPursuitRange;
   bool isWalkingback;
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   float distanceToPlayer;

   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      
   }

   void FixedUpdate(){
      if (_targetDetection.TargetIsDetected(_player.transform)){
         isOutOfPursuitRange = false;
      }
      if (isOutOfPursuitRange && !isWalkingback){
         GoBackToOriginalPosition();
         return;
      }
      isOutOfPursuitRange = _targetDetection.DistanceToTarget(_player.transform) > pursuitRange;
      
      if (!isOutOfPursuitRange){
         PursuitPlayer();
      }
   }

   void PursuitPlayer(){
      isWalkingback = false;
      Debug.Log("Pursuiting player");
   }

   void GoBackToOriginalPosition(){
      isWalkingback = true;
      Debug.Log("Go Back!!!!!");
   }

   
}
