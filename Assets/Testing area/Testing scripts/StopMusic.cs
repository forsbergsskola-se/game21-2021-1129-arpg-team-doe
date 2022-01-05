using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class StopMusic : MonoBehaviour{
   EventInstance _idleInstance;
   Fighter _fighter;

   void Start(){
      _idleInstance = GetComponentInChildren<EventInstance>();
      _fighter = GetComponent<Fighter>();
   }

   void LateUpdate(){
      if (!_fighter.IsIdle){
         _idleInstance.stop(STOP_MODE.IMMEDIATE);
      }
      else{
         _idleInstance.start();
      }
   }
}
