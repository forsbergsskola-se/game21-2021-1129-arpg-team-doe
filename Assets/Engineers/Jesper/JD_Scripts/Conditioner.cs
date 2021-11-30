using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditioner : MonoBehaviour
{
    internal bool completed = false;
    private void OnMouseDown()
    {
        completed = true;
    }
}
