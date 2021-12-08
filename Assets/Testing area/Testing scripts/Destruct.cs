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

    Statistics _statistics;

    float distance;
    bool isDestroyed;


    void Start(){
        _statistics =GameObject.FindWithTag("Player").GetComponent<Statistics>();
    }
    void OnMouseUpAsButton(){
        if (health<=0){
            isDestroyed = true;
        }
    }
    public void Destruction(int damage){ //Used for Debug
        health -= damage;
        if (health <= 0){
            isDestroyed = true;
        }
        if (isDestroyed){
            //animation here plxz
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Destruct>().enabled = false; // maybe useful?
        }
    }
}
