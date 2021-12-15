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
    [SerializeField] [Range(50,90)] float cameraAngle = 60f;
    
    Transform target;
    Transform camera;
    
    Vector3 offset;
    Vector3 rotationAxis = new Vector3(0, 1, 0);
    Vector3 snapRotationAxis = new Vector3(0, 1, 0);
    Vector3 _velocity = Vector3.zero;
    Vector3 _cameraRotation;
    
    float startZoom;
    float speed;
    
    int zoomLevel = 0;

    void Start(){
        target = FindObjectOfType<PlayerController>().transform;
        camera = GetComponentInChildren<Camera>().transform;
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        startZoom = transform.position.y;
        offset = transform.position - target.position;
        var rotation = camera.transform.rotation;
        _cameraRotation = rotation.eulerAngles;
        _cameraRotation.x = cameraAngle;
        rotation.eulerAngles = _cameraRotation;
        camera.transform.rotation = rotation;
    }

    void LateUpdate(){
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        CameraZoom();
        SnapCameraRotation();
        MouseCameraRotation();
        //AdjustAngle();
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

    void AdjustAngle(){
        
        
        //change the x-rotation of the camera
        var cameraRotation = camera.rotation;
        _cameraRotation = new Vector3(cameraAngle,0f,0f);
        cameraRotation.eulerAngles = _cameraRotation;
        //needs to be final line in rotation
        camera.rotation = cameraRotation;
        //Change the z distance on the followtarget
        var cameraOffset = offset.y;
        var cameraPosition = camera.position;
        cameraPosition.z = target.position.z-cameraOffset;
        //needs to be final line in position
        camera.position = cameraPosition;
        
        
        //use the same numbers as before to follow the player
        //when angle is lower the z position of parent needs to be more negative to compensate

    }
}
