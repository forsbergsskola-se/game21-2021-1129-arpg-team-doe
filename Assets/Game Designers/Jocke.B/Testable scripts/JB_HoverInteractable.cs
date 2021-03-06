using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_HoverInteractable : MonoBehaviour{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D interactableObject;
    
    Vector2 offSet = new(15, 15);
    void OnMouseEnter(){
        if (!GetComponent<JB_Conditions>().completed){
            Cursor.SetCursor(interactableObject, offSet , CursorMode.ForceSoftware);
        }
    }

    void OnMouseExit(){
        Cursor.SetCursor(defaultCursor, offSet , CursorMode.ForceSoftware);
    }
}
