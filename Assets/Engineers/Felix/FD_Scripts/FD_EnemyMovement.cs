using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{

   [SerializeField] float areaDetectionRange = 5.0f;
   [SerializeField] float visionRange = 20.0f;
   [SerializeField] float pursuitRange = 30f;
   [SerializeField] [Tooltip("0 to 360 degrees")] float viewAngle;

   bool _pursuit;
   FD_Player _player;
   float distanceToPlayer;

   void Start(){
      _player = FindObjectOfType<FD_Player>();
      viewAngle = Mathf.Cos(viewAngle * MathF.PI / 180 / 2);
   }

   void FixedUpdate(){
      if (PlayerIsDetected()){
         //Move towards
         _pursuit = true;
         if (distanceToPlayer < pursuitRange){
            
         }
      }
   }

   bool PlayerIsDetected(){
       //Distance check
      distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
      
      
      if (distanceToPlayer < areaDetectionRange){
         return true;
      }
      
      Vector3 playerDirection = _player.transform.position - transform.position;
      var dot = Vector3.Dot(playerDirection.normalized, transform.forward);
      if (distanceToPlayer < visionRange && dot > viewAngle){
         return true;
      }
      
      return false;
   }
}
