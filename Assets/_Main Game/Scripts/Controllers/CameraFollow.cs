using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    const float closeToRightScreenEdge = 0.95f;
    const float closeToLeftScreenEdge = 0.05f;
    const float rightScreenEdge = 1f;
    const float leftScreenEdge = 0f;
    const int zoomLevels = 4;
    const int cameraAngles = 3;

    [SerializeField] float smoothTime = 0.01f;
    [SerializeField] float mouseRotationSpeedMultiplier = 500f;
    [SerializeField] float keyboardRotationSpeedMultiplier = 30;
    [SerializeField] float minZoom = 3f;
    [SerializeField] float medZoom = 8f;
    [SerializeField] float maxZoom = 15f;
    [SerializeField] [Range(30,90)] float cameraAngleX;

    InventoryController _inventoryController;
    Transform target;
    Vector3 offset;
    Vector3 rotationAxis = new Vector3(0, 1, 0);
    Vector3 _velocity = Vector3.zero;

    float startZoom;

    float speed;
    int zoomLevel;
    int cameraAngle;

    void Awake(){
        _inventoryController = FindObjectOfType<InventoryController>();
    }

    void Start(){
        target = FindObjectOfType<PlayerController>().transform;
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        startZoom = transform.position.y;
        offset = new Vector3(0, transform.position.y - target.position.y, 0);
    }

    void LateUpdate(){
        // camera tilt:
        Vector3 adjustedOffset = Quaternion.Euler(90 - cameraAngleX, transform.rotation.eulerAngles.y - 180, 0) * offset;
        Vector3 targetPosition = target.position + adjustedOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);

        CameraZoom();

        if (_inventoryController.clickOnUI){
            return;
        }
        KeyboardCameraRotation();
        MouseCameraRotation();
        transform.eulerAngles = new Vector3(cameraAngleX, transform.rotation.eulerAngles.y,0f);
        CameraAngle();
    }

    void CameraZoom(){
        if (Input.GetKeyDown(KeyCode.Z)){
            var currentZoomLevel = zoomLevel % zoomLevels;
            //Change to minZoom
            if (currentZoomLevel == 0){
                offset.y = minZoom;
            }
            //Change to maxZoom
            else if (currentZoomLevel == 1){
                offset.y = medZoom;
            }
            else if (currentZoomLevel == 2)
            {
                offset.y = maxZoom;
            }
            //Change to startZoom
            else{
                offset.y = startZoom;
            }
            zoomLevel++;
        }
    }

    void KeyboardCameraRotation(){
        var keyboardRotation = rotationAxis * keyboardRotationSpeedMultiplier * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(keyboardRotation);
        }
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(-keyboardRotation);
        }
    }

    void MouseCameraRotation(){
        var mouseRotation = rotationAxis * mouseRotationSpeedMultiplier * Time.deltaTime;
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

    void CameraAngle(){
        if (Input.GetKeyDown(KeyCode.C)){
            var currentCameraAngle = cameraAngle % cameraAngles;
            //Change to lowest cameraAngle
            if (currentCameraAngle == 0){
                cameraAngleX = 30;
            }
            //Change to medium cameraAngle
            else if (currentCameraAngle == 1){
                cameraAngleX = 60;
            }
            //Change to highest cameraAngle
            else if (currentCameraAngle == 2)
            {
                cameraAngleX = 80;
            }
            cameraAngle++;
        }
    }
}
