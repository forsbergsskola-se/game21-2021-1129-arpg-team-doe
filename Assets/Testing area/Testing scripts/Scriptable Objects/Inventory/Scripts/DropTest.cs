using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTest : MonoBehaviour{
    [SerializeField] LootTable lootTable;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            ItemObject itemObject = lootTable.GetDrop();
            Debug.Log(itemObject.name);
        }
    }
}
