using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorOnDoor : MonoBehaviour{
    internal bool openable;

    [SerializeField] Texture2D lockedTexture;
    [SerializeField] Texture2D unLockedTexture;
    [SerializeField] Texture2D cursor;

    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter(){
        if (openable){
            Cursor.SetCursor(lockedTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        if (!openable){
            Cursor.SetCursor(unLockedTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    void OnMouseExit(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
