using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover_interactable : MonoBehaviour{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D interactableObject;
    void OnMouseEnter(){
        Cursor.SetCursor(interactableObject, -Vector2.one, CursorMode.Auto);
    }

    void OnMouseExit(){
        Cursor.SetCursor(defaultCursor,Vector2.zero, CursorMode.Auto);
    }
}
