using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_Conditioner : MonoBehaviour //Name: DoorConditions (maybe DoorCondition if only used for doors)
{
    internal bool completed;
    void OnMouseDown(){
        completed = true;
    }
}
