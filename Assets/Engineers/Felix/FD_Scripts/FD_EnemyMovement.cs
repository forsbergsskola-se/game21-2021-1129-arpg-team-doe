using System;
using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float pursuitRange = 30f;
   
   
   bool isOutOfPursuitRange;
   bool isWalkingback;
   bool playerIsDetected;
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   float distanceToPlayer;

   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      
   }

   void FixedUpdate(){
      //Detects player
      if (_targetDetection.TargetIsDetected(_player.transform)){
         playerIsDetected = true;
         isOutOfPursuitRange = false;
      }
      //If player is out of pursuit range and is not already walking back, walk back.
      if (isOutOfPursuitRange && !isWalkingback){
         GoBackToOriginalPosition();
         return;
      }
      //Checks if player is outside of the pursuit range (Returns true or false)
      isOutOfPursuitRange = _targetDetection.DistanceToTarget(_player.transform) > pursuitRange;
      
      //If player is not outside of range, pursuit player.
      if (!isOutOfPursuitRange && playerIsDetected){
         PursuitPlayer();
      }
   }

   void PursuitPlayer(){
      isWalkingback = false;
      Debug.Log("Pursuiting player");
   }

   void GoBackToOriginalPosition(){
      isWalkingback = true;
      playerIsDetected = false;
      Debug.Log("Go Back!!!!!");
   }

   
}
