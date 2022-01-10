using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingLady : MonoBehaviour
{
   AnimationController _animationController;

    void Start()
    {
        _animationController = GetComponent<AnimationController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(TurnThenVanish());
        }
    }

    IEnumerator TurnThenVanish()
    {
        _animationController.ChangeAnimationState("Turn");
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
