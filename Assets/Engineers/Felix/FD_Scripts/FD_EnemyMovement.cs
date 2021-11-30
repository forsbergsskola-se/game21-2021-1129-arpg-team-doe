using System;
using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{
   
   [SerializeField] float pursuitRange = 30f;
   
   
   bool _pursuit;
   FD_TargetDetection _targetDetection;
   FD_Player _player;
   float distanceToPlayer;

   void Start(){
      _player = FindObjectOfType<FD_Player>(); //maybe use player tag instead? Will save performance
      _targetDetection = GetComponent<FD_TargetDetection>();
      
   }

   void FixedUpdate(){
      if (_targetDetection.TargetIsDetected(_player.transform)){
         _pursuit = distanceToPlayer < pursuitRange;
         if (_pursuit) PursuitPlayer();
         else GoBackToOriginalPosition();
      }
   }

   void PursuitPlayer(){
      Debug.Log("Pursuiting player");
   }

   void GoBackToOriginalPosition(){
      Debug.Log("Go Back!!!!!");
   }

   
}
