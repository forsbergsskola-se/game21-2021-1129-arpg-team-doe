using System;
using UnityEngine;

public class FD_Fighter : MonoBehaviour
{
    [SerializeField] float attackRange = 2f;
    GameObject target;

    void Update(){
        throw new NotImplementedException();
    }


    public void Attack(){
        Debug.Log("attack");
    }
    
}
