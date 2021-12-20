using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour{
    [SerializeField] [Min(5)] float miniMapDistance;
    
    Transform _player;
    Transform _mainCamera;
    Vector3 newPosition;
    Camera thisCamera;

    void Start(){
        _player = FindObjectOfType<PlayerController>().transform;
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        thisCamera = GetComponent<Camera>();
        thisCamera.orthographicSize = miniMapDistance;
    }

    void LateUpdate(){
        newPosition = _player.position;
        var myTransform = transform;
        newPosition.y = myTransform.position.y;
        myTransform.position = newPosition;
        transform.rotation = Quaternion.Euler(myTransform.eulerAngles.x, _mainCamera.eulerAngles.y, 0f);
    }
}
