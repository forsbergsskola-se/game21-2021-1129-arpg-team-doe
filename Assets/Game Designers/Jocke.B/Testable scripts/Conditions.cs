using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour //Name: Conditions (maybe DoorCondition if only used for doors)
{
    internal bool completed;
    void OnMouseDown(){
        Debug.Log("KEY TAKEN");
        completed = true;
    }
}
