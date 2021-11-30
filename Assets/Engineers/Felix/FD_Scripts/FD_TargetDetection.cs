using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FD_TargetDetection : MonoBehaviour{
    
    
    [SerializeField] float areaDetectionRange = 5.0f;
    [SerializeField] float visionRange = 20.0f;
    [SerializeField] [Range(0,360)] float viewAngle;
    
    float distanceToTarget;

    void Start(){
        viewAngle = Mathf.Cos(viewAngle * MathF.PI / 180 / 2);
    }

    public float DistanceToTarget(Transform target){
        return Vector3.Distance(transform.position, target.position);
    }

    public bool TargetIsDetected(Transform target){
        //Distance check
        distanceToTarget = DistanceToTarget(target);

        if (distanceToTarget < areaDetectionRange){
            return true;
        }
      
        //Calculates the view angle and checks if unit is looking at target
        Vector3 targetDirection = target.position - transform.position;
        var dot = Vector3.Dot(targetDirection.normalized, transform.forward);
        if (distanceToTarget < visionRange && dot > viewAngle){
            return true;
        }
      
        return false;
    }
}
