using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetDetection : MonoBehaviour
{
   
    [SerializeField] float areaDetectionRange = 5.0f;
    [SerializeField] float visionRange = 20.0f;
    [SerializeField] [Range(0,360)] float viewAngle;
    
    float distanceToTarget;

    void Start(){
        viewAngle = Mathf.Cos(viewAngle * MathF.PI / 180 / 2);
    }
    
    public float DistanceToTarget(Vector3 position, Transform target){
        return Vector3.Distance(position, target.position);
    }
    
    public bool TargetIsDetected(Vector3 position, Transform target){
        //Distance check
        distanceToTarget = DistanceToTarget(position, target);
        Vector3 targetDirection = target.position - transform.position;
        if (distanceToTarget < areaDetectionRange){
            //If target is within detection area, shoot out a ray to see if target is visable
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection, out hit)){
                Debug.DrawRay(transform.position,targetDirection.normalized*hit.distance,Color.red);
                Debug.Log(hit.transform);
                if (hit.transform == target.transform){
                    return true; 
                }
            }
        }
        
        //Calculates the view angle and checks if unit is looking at target
        var dot = Vector3.Dot(targetDirection.normalized, transform.forward);
        if (distanceToTarget < visionRange && dot > viewAngle){ 
            //ray to check if something target is visable, if so, return true.
            RaycastHit hit;
            if(Physics.Raycast(transform.position,targetDirection, out hit)){ 
                Debug.DrawRay(transform.position,targetDirection.normalized*hit.distance,Color.red);
                Debug.Log(hit.transform);
                if (hit.transform == target.transform){ 
                    return true; 
                }
                else{ return false;
                }
            }
        }
        
        return false;
    }

   public bool TargetIsVisible(Vector3 position, Transform target, float attackRange){
        distanceToTarget = DistanceToTarget(position, target);
        Vector3 targetDirection = target.position - transform.position;
        if (distanceToTarget <= attackRange){
            //If target is within detection area, shoot out a ray to see if target is visable
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetDirection, out hit)){
                Debug.DrawRay(transform.position,targetDirection.normalized*hit.distance,Color.red);
                Debug.Log(hit.transform);
                if (hit.transform == target.transform){
                    return true; 
                }
            }
        }

        return false;
   }
   
}
