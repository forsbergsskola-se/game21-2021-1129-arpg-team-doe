using System;
using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float pursuitRange = 30f;
   
   
   bool isOutOfPursuitRange;
   bool isWalkingback;
   bool isPursuing;
   bool playerIsDetected;
   
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   float distanceToPlayer;
   Vector3 savedPosition;

   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      
   }

   void FixedUpdate(){

      if (!playerIsDetected && !isPursuing && !isWalkingback){
          savedPosition = transform.position;
          Debug.Log(savedPosition + "No player");
      }
      
      DetectPlayer();
      //If player is out of pursuit range and is not already walking back, walk back.
      if (isOutOfPursuitRange && !isWalkingback){
         GoBackToOriginalPosition();
         return;
      }
      //Checks if player is outside of the pursuit range (Returns true or false)
      isOutOfPursuitRange = _targetDetection.DistanceToTarget(savedPosition, _player.transform) > pursuitRange;
      
      //If player is not outside of range, pursuit player.
      if (!isOutOfPursuitRange && playerIsDetected){
         PursuitPlayer();
      }
   }

   void DetectPlayer(){
      if (_targetDetection.TargetIsDetected(transform.position, _player.transform)){
         playerIsDetected = true;
         if (!isPursuing && !isWalkingback){
            savedPosition = transform.position;
            Debug.Log(savedPosition + "Player found");
         }
         
         isOutOfPursuitRange = false;
      }
   }

   void PursuitPlayer(){
      isPursuing = true;
      isWalkingback = false;
      Debug.Log("Pursuiting player");
   }

   void GoBackToOriginalPosition(){
      isWalkingback = true;
      playerIsDetected = false;
      Debug.Log("Go Back!!!!!");
   }

   
}
