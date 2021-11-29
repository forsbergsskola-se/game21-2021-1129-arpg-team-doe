using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FD_CameraFollow : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float rotationSpeedMultiplier = 5f;
    [SerializeField] float snapRotationSpeedMultiplier = 15f;
    
    Vector3 rotationAxis = new Vector3(0, 1, 0);
    const float rightScreenEdge = 0.9f;
    const float leftScreenEdge = 0.1f;
    float speed;
    
    Vector3 _velocity = Vector3.zero;



    // Start is called before the first frame update
    void Start(){
        target = FindObjectOfType<FD_PlayerMovement>().transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate(){
        RotateCamera();
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }

    void RotateCamera(){
        
        var snapRotation = rotationAxis * snapRotationSpeedMultiplier;
        rotationAxis.y = 1;
        if (Input.GetKeyDown(KeyCode.E)){
            transform.Rotate(snapRotation);
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            transform.Rotate(-snapRotation);
        }
        
        var mouseRotation = rotationAxis * rotationSpeedMultiplier;
        var currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (currentMousePosition.x > rightScreenEdge){
            rotationAxis.y = currentMousePosition.x - rightScreenEdge;
            transform.Rotate(mouseRotation);
        }
        if (currentMousePosition.x < leftScreenEdge){
            rotationAxis.y = currentMousePosition.x - leftScreenEdge;
            transform.Rotate(mouseRotation);
        }
        Debug.Log(currentMousePosition);
    }
    
    
    
}
