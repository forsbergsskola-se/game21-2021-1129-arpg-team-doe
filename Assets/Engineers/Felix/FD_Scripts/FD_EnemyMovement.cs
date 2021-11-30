using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float pursuitRange = 30f;
   
   
   bool isOutOfPursuitRange;
   bool isWalkingback;
   bool isPursuing;
   bool playerIsDetected;
   
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   FD_NavmeshMover _navmeshMover;
   
   float distanceToPlayer;
   Vector3 savedPosition;

   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      _navmeshMover = GetComponent<FD_NavmeshMover>();
   }

   void FixedUpdate(){
      if (!playerIsDetected && !isPursuing && !isWalkingback){
          savedPosition = transform.position;
      }
      
      DetectPlayer();
      //If player is out of pursuit range and is not already walking back, walk back.
      if (isOutOfPursuitRange && !isWalkingback){
         GoBackToOriginalPosition();
         return;
      }
      //Checks if the enemy is outside of the pursuit range (Returns true or false)
      isOutOfPursuitRange = _targetDetection.DistanceToTarget(savedPosition, transform) > pursuitRange;
      
      //If player is not outside of range, pursuit player.
      if (!isOutOfPursuitRange && playerIsDetected){
         PursuitPlayer();
      }
   }

   void DetectPlayer(){
      // Enemy stands for a while with tranform.postion and then go back, can be changed to savedPosition to go back immediately
      if (_targetDetection.TargetIsDetected(savedPosition, _player.transform)){
         playerIsDetected = true;
         if (!isPursuing && !isWalkingback){
            savedPosition = transform.position;
         }
         isOutOfPursuitRange = false;
      }
      
   }

   void PursuitPlayer(){
      isPursuing = true;
      isWalkingback = false;
      _navmeshMover.Mover(_player.transform.position);
   }

   void GoBackToOriginalPosition(){
      playerIsDetected = false;
      _navmeshMover.Mover(savedPosition);
      isWalkingback = true;
      isPursuing = false;
   }
}
