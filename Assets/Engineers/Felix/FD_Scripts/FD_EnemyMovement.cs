using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FD_EnemyMovement : MonoBehaviour{

   [SerializeField] float areaDetectionRange = 5.0f;
   [SerializeField] float visionRange = 20.0f;
   [SerializeField] [Tooltip("1 is no angle between. 0 is looking 100% away")] float viewAngle;



   FD_Player _player;

   void Start(){
      _player = FindObjectOfType<FD_Player>();
      viewAngle = Mathf.Cos(viewAngle * MathF.PI / 180 / 2);
   }

   void FixedUpdate(){
      if (PlayerIsDetected()){
         //Move towards
      }
   }

   bool PlayerIsDetected(){
       //Distance check
      var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
      
      
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
