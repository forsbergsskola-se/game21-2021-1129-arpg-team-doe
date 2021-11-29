using UnityEngine;

public class FD_CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float rotationSpeedMultiplier = 5f;
    [SerializeField] float snapRotationSpeedMultiplier = 30f;
    [SerializeField] float minZoom = 3f;
    [SerializeField] float maxZoom = 15f;
    
    Vector3 offset;
    Vector3 rotationAxis = new Vector3(0, 1, 0);
    Vector3 snapRotationAxis = new Vector3(0, 1, 0);
    const float rightScreenEdge = 0.95f;
    const float leftScreenEdge = 0.05f;
    float speed;
    
    Vector3 _velocity = Vector3.zero;

    void Start(){
        target = FindObjectOfType<FD_PlayerMovement>().transform;
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
        if (Input.GetKeyDown(KeyCode.W)){
            offset.y = minZoom;
        }
        if (Input.GetKeyDown(KeyCode.S)){
            offset.y = maxZoom;
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
        var mouseRotation = rotationAxis * rotationSpeedMultiplier;
        var currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (currentMousePosition.x > rightScreenEdge){
            rotationAxis.y =   currentMousePosition.x - rightScreenEdge;
            transform.Rotate(mouseRotation);
        }
        if (currentMousePosition.x < leftScreenEdge){
            rotationAxis.y =   currentMousePosition.x - leftScreenEdge;
            transform.Rotate(mouseRotation);
        }
    }

}
