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
    [SerializeField] Vector3 rotationSpeed = new Vector3(0, 15, 0);
    
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
        if (Input.GetKeyDown(KeyCode.E)){
            transform.Rotate(rotationSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            transform.Rotate(-rotationSpeed);
        }

        var currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (currentMousePosition.x > 0.9f){
            transform.Rotate(rotationSpeed);
            //Not Done
        }
        if (currentMousePosition.x < 0.1f){
            transform.Rotate(-rotationSpeed);
            //Not Done
        }
        
        
        Debug.Log(currentMousePosition);

    }
    
    
    
}
