using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class UnlockDoor : MonoBehaviour
{
    private bool locked = true;
    private bool conditionCompleted;
    [SerializeField] float actionRange;
    
    
    
    void Update()
    {
        conditionCompleted = FindObjectOfType<Conditioner>().completed;
        LockingMechanism();
        if (locked)
        {
            FindObjectOfType<CursorOnDoor>().unOpenable = locked;
        }
        if (!locked)
        {
            FindObjectOfType<CursorOnDoor>().unOpenable = locked;
        }
        

    }

    void LockingMechanism()
    {
        if (conditionCompleted)
        {
            locked = false;
        }
        else
        {
            locked = true;
        }
    }

    private void OnMouseDown()
    {

        if (!locked){
            GetComponent<BoxCollider>().enabled = false;
        }
    }
   
    
}
