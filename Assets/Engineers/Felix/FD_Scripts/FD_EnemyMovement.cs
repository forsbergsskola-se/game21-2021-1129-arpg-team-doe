using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float aggroRange = 30f;
   [SerializeField] float attackRange = 5f;
   bool isOutsideOfCombatRange;
   bool isWalkingback;
   bool isPursuing;
   bool targetIsDetected;
   
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

   void Update(){

      //TODO: Saved position does not work the way we want it to.
      if (UpdateSavePosition()){
         savedPosition = transform.position;
         Debug.Log(savedPosition);
      }

      ResetBools();
      
      DetectTarget();
      
      //If player is out of pursuit range and is not already walking back, walk back.
      // if (isOutsideOfCombatRange && !isWalkingback){
      //    GoBackToOriginalPosition();
      //    return;
      // }
      
      //Checks if the enemy is outside of the pursuit range (Returns true or false)
      
      //Calculates distance from enemy position to player
      distanceToPlayer = _targetDetection.DistanceToTarget(transform.position, _player.transform);

      if (OutsideOfMaxAttackRange()){
         GoBackToOriginalPosition();
         return;
      }
      if (CheckEnemyIsInAttackRange()){
         
         if (_targetDetection.TargetIsVisible(transform.position, _player.transform, attackRange)){
            //Attack
            _navmeshMover.StopMoving();
            Debug.Log("I AM ATTACKING" + _player.name);
            return;
         }
         
      }

      if (targetIsDetected){
         PursuitTarget();
      }
      else{
         GoBackToOriginalPosition();
      }
      // if (distanceToPlayer > attackRange){
      //    //If player is not outside of range, pursuit player.
      //    if (!isOutsideOfCombatRange && targetIsDetected){
      //       PursuitTarget();
      //    }
      // }
     
      

   }


   void ResetBools(){
      if (isWalkingback && _targetDetection.DistanceToTarget(savedPosition, transform) < 1){
         isWalkingback = false;
         targetIsDetected = false;
         isPursuing = false;
         Debug.Log("Resetting Bools");
      }
   }

   bool CanResetWalkingback(){
      return (isWalkingback && _targetDetection.DistanceToTarget(savedPosition, transform) < 0.001);
   }

   bool OutsideOfMaxAttackRange(){
      return isOutsideOfCombatRange = _targetDetection.DistanceToTarget(savedPosition, transform) > aggroRange;
   }

   bool CheckEnemyIsInAttackRange(){
      if (distanceToPlayer < attackRange){
         return true;
      }
      return false;
   }
   
   

   bool UpdateSavePosition(){
      if (!isPursuing && !isWalkingback){
         return true;
      }
      return false;
   }
   

   void DetectTarget(){
      // Enemy stands for a while with tranform.postion and then go back, can be changed to savedPosition to go back immediately
      if (_targetDetection.TargetIsDetected(savedPosition, _player.transform)){
         targetIsDetected = true;
         if (!isPursuing && !isWalkingback){
            savedPosition = transform.position;
         }
         isOutsideOfCombatRange = false;
      }
      
   }

   void PursuitTarget(){
      isPursuing = true;
      isWalkingback = false;
      _navmeshMover.Mover(_player.transform.position);
   }

   public void GoBackToOriginalPosition(){
      targetIsDetected = false;
      _navmeshMover.Mover(savedPosition);
      isWalkingback = true;
      isPursuing = false;
      
   }
}
