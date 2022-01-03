using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropTest : MonoBehaviour{
    [SerializeField] LootTable lootTable;
    [SerializeField] GameObject cash;
    [SerializeField] float spreadRange;
    [SerializeField] int maxRollCount = 2;
    Statistics _playerStatistics;

    int rollCount;
    

    void Awake(){
        _playerStatistics = GameObject.FindWithTag("Player").GetComponent<Statistics>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            InstantiateItem();
        }
    }

    void InstantiateItem(){

        if (rollCount < maxRollCount){
            GameObject itemObject = lootTable.GetDropItem();
            Instantiate(itemObject, transform.position + RandomLocation(), Quaternion.identity);
            Instantiate(cash, transform.position + RandomLocation(), Quaternion.identity);
            rollCount++;

            if (Random.Range(0f, 1f) < _playerStatistics.LuckChance){
                InstantiateItem();
                //PlayLuckSound();
            }
        }
    }

    Vector3 RandomLocation(){
        //may change x and z between 1-3?
        var position = new Vector3((Random.insideUnitSphere.x * spreadRange), 0f,
            (Random.insideUnitSphere.z * spreadRange));
        return position;
    }
}
