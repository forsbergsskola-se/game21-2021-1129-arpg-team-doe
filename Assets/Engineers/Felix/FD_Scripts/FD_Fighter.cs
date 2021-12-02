using UnityEngine;

public class FD_Fighter : MonoBehaviour
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
