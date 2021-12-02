using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] internal float attackRange = 2f;
    GameObject target;

    void Update(){
       
    }

    public void Attack(Transform target){
        Debug.Log(transform.name + " Attacking " + target.name);
    }

    public void StopAttack(Transform target){
        Debug.Log(transform.name + " Stop attacking " + target.name);
    }

}
