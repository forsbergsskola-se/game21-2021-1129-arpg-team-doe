using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTesting : MonoBehaviour
{
    [SerializeField] UnityEvent OnCollision;


    void OnCollisionStay(Collision other){
       OnCollision.Invoke();
    }
}
