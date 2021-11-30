using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class JD_UnlockDoor : MonoBehaviour{

    NavMeshAgent player;
    bool locked = true;
    bool conditionCompleted;
    [SerializeField] float actionRange;

    float distance;

    void Start(){
        player = FindObjectOfType<NavMeshAgent>();
    }

    void Update(){
        conditionCompleted = FindObjectOfType<Conditioner>().completed;
        LockingMechanism();
        // Does the same thing.
        if (locked){
            FindObjectOfType<CursorOnDoor>().unOpenable = locked;
        } if (!locked){
            FindObjectOfType<CursorOnDoor>().unOpenable = locked;
        }
        distance = Vector3.Magnitude(player.destination);
    }

    void LockingMechanism(){
        if (conditionCompleted){
            locked = false;
        }else{
            locked = true;
        }
    }

    void OnMouseDown(){
        if (!locked && distance > actionRange){
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
