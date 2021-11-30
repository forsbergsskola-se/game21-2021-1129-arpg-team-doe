using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class UnlockDoor : MonoBehaviour{

    private NavMeshAgent player;
    private bool locked = true;
    private bool conditionCompleted;
    [SerializeField] private float actionRange;

    private float distance;

    private void Start(){
        player = FindObjectOfType<NavMeshAgent>();
    }

    void Update(){

        conditionCompleted = FindObjectOfType<Conditioner>().completed;
        LockingMechanism();
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

    private void OnMouseDown(){

        if (!locked && distance > actionRange){
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
