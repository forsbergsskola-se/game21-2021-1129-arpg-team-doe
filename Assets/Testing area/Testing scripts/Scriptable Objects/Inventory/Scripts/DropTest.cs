using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTest : MonoBehaviour{
    [SerializeField] LootTable lootTable;
    [SerializeField] GameObject cash;
    [SerializeField] float spreadRange;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            GameObject itemObject = lootTable.GetDrop();
            Instantiate(itemObject, transform.position + RandomLocation(), Quaternion.identity);
            Instantiate(cash, transform.position + RandomLocation(), Quaternion.identity);
            Debug.Log(itemObject.name);
        }
    }
    
    Vector3 RandomLocation(){
        //may change x and z between 1-3?
        var position = new Vector3((Random.insideUnitSphere.x * spreadRange), 0f,
            (Random.insideUnitSphere.z * spreadRange));
        return position;
    }
}
