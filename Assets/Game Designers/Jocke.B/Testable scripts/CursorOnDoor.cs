using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorOnDoor : MonoBehaviour
{
    internal bool openable;

    public Texture2D lockedTexture;
    public Texture2D unLockedTexture;
    public Texture2D cursor;

    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter(){
        if (openable){
            Cursor.SetCursor(unLockedTexture, new Vector2(), CursorMode.Auto);
        }
        if (!openable){
            Cursor.SetCursor(lockedTexture, new Vector2(), CursorMode.Auto);
        }
    }

    void OnMouseExit(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
