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
       // OnMouseDown();
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

  //  private void OnMouseDown()
  //  {
    //    GetComponent<BoxCollider>().enabled = false;
    //}
   
}
