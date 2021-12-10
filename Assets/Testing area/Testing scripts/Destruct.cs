using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDestructible{
    void Destruction(int damage);

}
public class Destruct : MonoBehaviour,IDestructible{
    
    [SerializeField] int health;
    [SerializeField] Mesh _spriteIntact;
    [SerializeField] Mesh _spriteDestroyed;

    float distance;
    bool isDestroyed;
    
    void Start(){
        GetComponent<MeshFilter>().mesh = _spriteIntact;
    }
    public void Destruction(int damage){ //Used for Debug
        health -= damage;
        if (health <= 0){
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
