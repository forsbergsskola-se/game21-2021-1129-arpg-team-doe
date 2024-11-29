using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_Conditions : MonoBehaviour //Name: DoorConditions (maybe DoorCondition if only used for doors)
{
    internal bool completed;
    void OnMouseDown(){
        Debug.Log("KEY TAKEN");
        completed = true;
    }
}
