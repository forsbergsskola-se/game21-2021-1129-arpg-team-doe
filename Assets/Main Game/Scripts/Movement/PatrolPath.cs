using System;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    const float WaypointGizmosRadius = 0.3f;
    void OnDrawGizmos(){
        for (int i = 0; i < transform.childCount; i++){
            int j = GetNextWaypointIndex(i);
            Gizmos.DrawSphere(transform.GetChild(i).position, WaypointGizmosRadius);
            Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
        }
    }
    
    public int GetNextWaypointIndex(int i){
        if (i + 1 == transform.childCount){
            return 0;
        }
        return i + 1;
    }

    public Vector3 GetWaypointPosition(int i){
        return transform.GetChild(i).position;
    }
}
