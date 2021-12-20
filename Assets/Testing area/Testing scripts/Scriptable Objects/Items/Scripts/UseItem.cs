using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour{
    ItemObject _itemObject;

    void Update(){
        if (Input.GetMouseButtonDown(1)){
            _itemObject.UseItem();
        }
    }
}
