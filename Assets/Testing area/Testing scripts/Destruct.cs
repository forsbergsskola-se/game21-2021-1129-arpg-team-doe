using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDestructible{
    void Destruction();

}
public class Destruct : MonoBehaviour,IDestructible{
     
    [SerializeField] Mesh _spriteIntact;
    [SerializeField] Mesh _spriteDestroyed;
    
    Statistics _statistics;
    float distance;
    bool isDestroyed;
    
    void Start(){
        GetComponent<MeshFilter>().mesh = _spriteIntact;
    }
    public void Destruction(){ //Used for Debug
        if (!_statistics.IsAlive){
            isDestroyed = true;
        }
        if (isDestroyed){
            DeactivateComponents();
        }
    }

    void DeactivateComponents(){
        GetComponent<MeshFilter>().mesh = _spriteDestroyed;
        GetComponent<NavMeshObstacle>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Destruct>().enabled = false;
        GetComponent<HoverInteractable>().enabled = false;
    }
}
