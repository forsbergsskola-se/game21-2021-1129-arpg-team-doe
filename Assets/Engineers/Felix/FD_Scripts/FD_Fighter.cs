using System;
using UnityEngine;

public class FD_Fighter : MonoBehaviour
{
    [SerializeField] internal float attackRange = 2f;
    GameObject target;

    void Update(){
       
    }


    public void Attack(){
        Debug.Log("attack");
    }
    
}
