using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    const float closeToRightScreenEdge = 0.95f;
    const float closeToLeftScreenEdge = 0.05f;
    const float rightScreenEdge = 1f;
    const float leftScreenEdge = 0f;
    const int zoomLevels = 3;
    
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float rotationSpeedMultiplier = 500f;
    [SerializeField] float snapRotationSpeedMultiplier = 30f;
    [SerializeField] float minZoom = 3f;
    [SerializeField] float maxZoom = 15f;
    
    Transform target;
    
    Vector3 offset;
    Vector3 rotationAxis = new Vector3(0, 1, 0);
    Vector3 snapRotationAxis = new Vector3(0, 1, 0);
    Vector3 _velocity = Vector3.zero;
    
    float startZoom;
    float speed;
    
    int zoomLevel = 0;

    void Start(){
        target = FindObjectOfType<PlayerMovement>().transform;
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z); //inefficient
        startZoom = transform.position.y;
        offset = transform.position - target.position;
    }

    void LateUpdate(){
        Vector3 targetPosition = target.position + offset; 
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        CameraZoom();
        SnapCameraRotation();
        MouseCameraRotation();
    }

    void CameraZoom(){
        
        if (Input.GetKeyDown(KeyCode.R)){
            var currentZoomLevel = zoomLevel % zoomLevels;
            //Change to minZoom
            if (currentZoomLevel == 0){
                offset.y = minZoom;
            }
            //Change to maxZoom
            else if (currentZoomLevel == 1){
                offset.y = maxZoom;
            }
            //Change to startZoom
            else{ 
                offset.y = startZoom;
            }
            zoomLevel++;
        }
    }

    void SnapCameraRotation(){
        snapRotationAxis.y = 1;
        var snapRotation = snapRotationAxis * snapRotationSpeedMultiplier;
        
        if (Input.GetKeyDown(KeyCode.D)){
            transform.Rotate(snapRotation); 
        }
        if (Input.GetKeyDown(KeyCode.A)){
            transform.Rotate(-snapRotation);
        }
    }

    void MouseCameraRotation(){ 
        var mouseRotation = rotationAxis * rotationSpeedMultiplier * Time.deltaTime;
        var currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (currentMousePosition.x > closeToRightScreenEdge && currentMousePosition.x < rightScreenEdge){
            rotationAxis.y =   currentMousePosition.x - closeToRightScreenEdge;
            transform.Rotate(mouseRotation);
        }
        if (currentMousePosition.x < closeToLeftScreenEdge && currentMousePosition.x > leftScreenEdge){
            rotationAxis.y =   currentMousePosition.x - closeToLeftScreenEdge;
            transform.Rotate(mouseRotation);
        }
    }
}
