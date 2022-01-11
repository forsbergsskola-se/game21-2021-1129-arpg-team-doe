using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class VanishingLady : MonoBehaviour
{
   AnimationController _animationController;
   [SerializeField] EventReference soundWhileTurning;
   [SerializeField] EventReference soundWhenVanishing;
   
   EventInstance soundWhileTurningInstance;
   EventInstance soundWhenVanishingInstance;

    void Start(){
        _animationController = GetComponent<AnimationController>();
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            StartCoroutine(TurnThenVanish());
        }
    }

    IEnumerator TurnThenVanish()
    {
        
        // //soundWhileTurningInstance = RuntimeManager.CreateInstance(soundWhileTurning);
        // soundWhenVanishingInstance = RuntimeManager.CreateInstance(soundWhenVanishing);
        // soundWhileTurningInstance.start();
        // soundWhileTurningInstance.release();
        _animationController.ChangeAnimationState("Turn");
        yield return new WaitForSeconds(3);
        // soundWhenVanishingInstance.start();
        // soundWhenVanishingInstance.release();
        gameObject.SetActive(false);
    }
}
