using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float aggroRange = 30f;
   [SerializeField] float attackRange = 5f;
   bool isOutOfCombatRange;
   bool isWalkingback;
   bool isPursuing;
   bool playerIsDetected;
   
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   FD_Movement _navmeshMover;
   
   float distanceToPlayer;
   Vector3 savedPosition;

   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      _navmeshMover = GetComponent<FD_Movement>();
   }

   void FixedUpdate(){
      if (!playerIsDetected && !isPursuing && !isWalkingback){
          savedPosition = transform.position;
      }
      
      DetectPlayer();
      //If player is out of pursuit range and is not already walking back, walk back.
      if (isOutOfCombatRange && !isWalkingback){
         GoBackToOriginalPosition();
         return;
      }
      //Checks if the enemy is outside of the pursuit range (Returns true or false)
      isOutOfCombatRange = _targetDetection.DistanceToTarget(savedPosition, transform) > aggroRange;
      distanceToPlayer = _targetDetection.DistanceToTarget(transform.position, _player.transform);
      if (distanceToPlayer > attackRange){
         //If player is not outside of range, pursuit player.
         if (!isOutOfCombatRange && playerIsDetected){
            PursuitPlayer();
         }
      }
      else{
         _navmeshMover.StopMoving();
      }

   }

   void DetectPlayer(){
      // Enemy stands for a while with tranform.postion and then go back, can be changed to savedPosition to go back immediately
      if (_targetDetection.TargetIsDetected(savedPosition, _player.transform)){
         playerIsDetected = true;
         if (!isPursuing && !isWalkingback){
            savedPosition = transform.position;
         }
         isOutOfCombatRange = false;
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
