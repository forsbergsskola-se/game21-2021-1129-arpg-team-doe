using UnityEngine;

public class FD_Fighter : MonoBehaviour //Used for debug

{
    
    GameObject target;
    
    public void Attack(Transform target){
        Debug.Log(transform.name + " Attacking " + target.name);
    }

    public void StopAttack(Transform target){
        Debug.Log(transform.name + " Stop attacking " + target.name);
    }
    
}
