using System;
using UnityEngine;

[RequireComponent(typeof(FD_TargetDetection))]
[RequireComponent(typeof(FD_Fighter))]
public class FD_Enemy : MonoBehaviour
{
    [SerializeField] float attackRange = 2f;
    FD_TargetDetection _targetDetection;
    FD_Fighter _attack;
    FD_Movement _movement;
    FD_EnemyMovement _enemyMovement;
    GameObject _player;
    

    void Start(){
        _targetDetection = GetComponent<FD_TargetDetection>();
        _player = FindObjectOfType<FD_Player>().gameObject;
        _movement = GetComponent<FD_Movement>();
        _enemyMovement = GetComponent<FD_EnemyMovement>();
        _attack = GetComponent<FD_Fighter>();
    }

    void Update(){
        if(IsChasingPlayer(_player) && !IsInAttackRange(_player)){ //TODO: and player can be attacked
           _movement.Mover(_player.transform.position);
        }
        
        if (IsInAttackRange(_player)){
            _movement.StopMoving();
            _attack.Attack();
        }
        
        if (!IsChasingPlayer(_player)){
            _enemyMovement.GoBackToOriginalPosition();
        }
    }

    bool IsChasingPlayer(GameObject player){
        return true; // TODO
    }

    bool IsInAttackRange(GameObject player){
        return true; // TODO
    }
}
