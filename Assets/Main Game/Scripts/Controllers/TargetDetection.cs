using System;
using FMOD.Studio;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        var targetDirection = DistanceAndDirectionCheck(position, target);
        
        if (AreaDetection(target, targetDirection)) return true;
        
        if (FrontalDetection(target, targetDirection)) return true;
        return false;
    }

    Vector3 DistanceAndDirectionCheck(Vector3 position, Transform target){
        distanceToTarget = DistanceToTarget(position, target);
        Vector3 targetDirection = target.position - transform.position;
        return targetDirection;
    }

    bool AreaDetection(Transform target, Vector3 targetDirection){
        if (distanceToTarget < areaDetectionRange){
            //If target is within detection area, shoot out a ray to see if target is visable
            if (RaycastCheck(target, targetDirection, Color.green)) return true;
        }

        return false;
    }

    bool FrontalDetection(Transform target, Vector3 targetDirection){
        //Calculates the view angle and checks if unit is looking at target
        var dot = Vector3.Dot(targetDirection.normalized, transform.forward);
        if (distanceToTarget < visionRange && dot > viewAngle){
            if (RaycastCheck(target, targetDirection, Color.red)) return true;
        }

        return false;
    }

    bool RaycastCheck(Transform target, Vector3 targetDirection, Color color){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDirection, out hit)){
            Debug.DrawRay(transform.position, targetDirection.normalized * hit.distance, color);
            if (hit.transform == target.transform){
                return true;
            }
            return false;
        }
        return false;
    }

    //The #if means when we build, it will ignore this, which would otherwise cause errors because unity editor things cannot be built.
    #if UNITY_EDITOR
    void OnDrawGizmosSelected(){
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position,transform.up, areaDetectionRange);
    }
    #endif
}
