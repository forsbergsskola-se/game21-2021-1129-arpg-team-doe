using UnityEngine;

public class Conditions : MonoBehaviour //Name: Conditions (maybe DoorCondition if only used for doors)
{
    [SerializeField] float objectPickupDistance = 3f;
    internal bool completed;
    GameObject _player;

    void Start(){
        _player = GameObject.FindWithTag("Player");
    }

    void OnMouseDown(){
        float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer < objectPickupDistance){ // TODO: make this into one action that we don't need to click again
            Debug.Log("KEY TAKEN");
            completed = true;
        }
    }
}
