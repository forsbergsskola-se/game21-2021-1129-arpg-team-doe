using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class StopIdleSound : MonoBehaviour
{
   StudioEventEmitter _eventEmitter;
   EventInstance _idleInstance;
   public Fighter _fighter; //sorry but this works and idk why and im sad and tired.

   void Start(){
      _eventEmitter = GetComponentInChildren<StudioEventEmitter>();
      _idleInstance = _eventEmitter.EventInstance;
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
