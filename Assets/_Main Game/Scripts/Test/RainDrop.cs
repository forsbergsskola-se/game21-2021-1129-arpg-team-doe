using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour, IPooledObject
{
    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
       this.gameObject.SetActive(true);
       _rigidbody.velocity = new Vector3(0,0,0);
    }

    // void OnTriggerEnter(Collider collider)
    // {
    //     this.gameObject.SetActive(false);
    // }
    void OnCollisionEnter(Collision Collider)
    {
        
        this.gameObject.SetActive(false);
    }
}
