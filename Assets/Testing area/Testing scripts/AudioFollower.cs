using System;
using UnityEngine;

public class AudioFollower : MonoBehaviour{
    [SerializeField] Vector3 offset;
    
    Transform player;
    Transform camera;

    void Start(){
        player = GetComponentInParent<PlayerController>().transform;
        camera = GetComponentInParent<CameraFollow>().transform;
    }

    void Update(){
        var transform1 = transform;
        transform1.position = player.position + offset;
        var transform1Rotation = transform1.rotation;
        transform1Rotation.eulerAngles = camera.rotation.eulerAngles;
        transform.position = transform1.position;
        transform.rotation = transform1.rotation;
    }
}
