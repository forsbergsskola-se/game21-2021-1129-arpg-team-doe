using UnityEngine;

public class FD_Attack : MonoBehaviour
{
    [SerializeField] float attackRange = 2f;
    GameObject target;
    

    void Update(){
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < attackRange){
            Attack();
        }
    }

    void Attack(){
        Debug.Log("attack");
    }
    
}
