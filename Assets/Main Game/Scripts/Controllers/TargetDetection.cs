using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class TargetDetection : MonoBehaviour{
    [SerializeField] float areaDetectionRange = 5.0f;
    [SerializeField] float visionRange = 20.0f;
    [SerializeField] [Range(0,360)] float viewAngle;
    float _distanceToTarget;
    float _viewAngleInRadius;

    void Start(){
        _viewAngleInRadius = Mathf.Cos(viewAngle * MathF.PI / 180 / 2);
    }
    
    public float DistanceToTarget(Vector3 position, Transform target){
        return Vector3.Distance(position, target.position);
    }
    
    public bool TargetIsDetected(Vector3 position, Transform target){
        var targetDirection = DistanceAndDirectionCheck(position, target);
        
        if (AreaDetection(target, targetDirection)){ return true;}
        if (FrontalDetection(target, targetDirection)){ return true;}
        return false;
    }

    Vector3 DistanceAndDirectionCheck(Vector3 position, Transform target){
        _distanceToTarget = DistanceToTarget(position, target);
        Vector3 targetDirection = target.position - transform.position;
        return targetDirection;
    }

    bool AreaDetection(Transform target, Vector3 targetDirection){
        if (_distanceToTarget < areaDetectionRange){
            //If target is within detection area, shoot out a ray to see if target is visable
            if (RaycastCheck(target, targetDirection, Color.green)){ return true;}
        }
        return false;
    }

    bool FrontalDetection(Transform target, Vector3 targetDirection){
        //Calculates the view angle and checks if unit is looking at target
        var dot = Vector3.Dot(targetDirection.normalized, transform.forward);
        if (_distanceToTarget < visionRange && dot > _viewAngleInRadius){
            if (RaycastCheck(target, targetDirection, Color.red)){ return true;}
        }
        return false;
    }

    bool RaycastCheck(Transform target, Vector3 targetDirection, Color color){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDirection, out hit)){
            if (hit.transform == target.transform){ return true;}
            return false;
        }
        return false;
    }

    //The #if means when we build, it will ignore this, which would otherwise cause errors because unity editor things cannot be built.
    #if UNITY_EDITOR
    void OnDrawGizmosSelected(){
        Handles.color = Color.yellow;
        var position = transform.position;
        Handles.DrawWireDisc(position,transform.up, areaDetectionRange);

        Gizmos.color = Color.cyan;
        DrawFrontalDetection(position, transform.forward, viewAngle, visionRange);
    }

    void DrawFrontalDetection(Vector3 position, Vector3 dir, float viewsAngle, float visionsRange, float segments = 20){
        var srcAngles = GetAnglesFromDir(position, dir);
        var initialPos = position;
        var posA = initialPos;
        var stepAngles = viewsAngle / segments;
        var angle = srcAngles - viewsAngle / 2;
        for (var i = 0; i <= segments; i++){
            var rad = Mathf.Deg2Rad * angle;
            var posB = initialPos;
            posB += new Vector3(visionsRange * Mathf.Cos(rad), 0, visionsRange * Mathf.Sin(rad));
            Gizmos.DrawLine(posA, posB);
            angle += stepAngles;
            posA = posB;
        }
        
        Gizmos.DrawLine(posA, initialPos);
    }

    float GetAnglesFromDir(Vector3 position, Vector3 dir){
        var forwardLimitPos = position + dir;
        var srcAngles = Mathf.Rad2Deg * Mathf.Atan2(forwardLimitPos.z - position.z, forwardLimitPos.x - position.x);
        return srcAngles;
    }
    #endif
}