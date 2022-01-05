using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class StopMusic : MonoBehaviour{
   EventInstance idleInstance;
   Fighter _fighter;

   void Start(){
      idleInstance = GetComponentInChildren<EventInstance>();
      _fighter = GetComponent<Fighter>();
   }

   void Update(){
      if (!_fighter.isIdle){
         idleInstance.stop(STOP_MODE.IMMEDIATE);
      }
      else{
         idleInstance.start();
      }
   }
}
