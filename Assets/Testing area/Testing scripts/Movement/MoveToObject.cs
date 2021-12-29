using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//===================
//WIP not yet done
public class MoveToObject : MonoBehaviour{
    Movement _movement;
    GameObject _object;

    bool isObstacle;

    void Start(){
        _movement = FindObjectOfType<Movement>();
        _object = gameObject;
        isObstacle = GetComponent<NavMeshObstacle>() != null;
    }

    void Update(){
      //  _movement.Mover(new Vector3(-3.67f, 0.72f, 2.15f));
    }

    void OnMouseDown(){
        if (isObstacle){
            Debug.Log("walking here!");
            Debug.Log(_object.transform.position - new Vector3(0,_movement.transform.position.y,-2));
            _movement.Mover(_object.transform.position - new Vector3(0,0,-2), 1f);
        }
        else if (!isObstacle){
            Debug.Log("stopping");
            _movement.StopMoving();
        }
    }
}
