using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class StopMusic : MonoBehaviour{
   EventInstance _idleInstance;
   public Fighter _fighter; //sorry but this works and idk why and im sad and tired.

   void Start(){
      _idleInstance = GetComponentInChildren<EventInstance>();
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
