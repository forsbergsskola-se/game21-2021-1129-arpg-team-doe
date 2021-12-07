using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Fighter : MonoBehaviour
{
    GameObject target;
    
    public void Attack(Transform target){
        Debug.Log(transform.name + " Attacking " + target.name);
    }

    public void StopAttack(Transform target){
        Debug.Log(transform.name + " Stop attacking " + target.name);
    }
}
