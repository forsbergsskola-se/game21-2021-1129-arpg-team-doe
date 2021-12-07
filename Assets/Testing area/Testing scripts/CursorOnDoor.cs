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

    Vector2 offSet = new(15, 15);

    void Start(){
        Cursor.SetCursor(cursor, offSet, CursorMode.ForceSoftware);
    }

    void OnMouseEnter(){
        if (openable){
            Cursor.SetCursor(lockedTexture, offSet, CursorMode.ForceSoftware);
        }
        if (!openable){
            Cursor.SetCursor(unLockedTexture, offSet, CursorMode.ForceSoftware);
        }
    }

    void OnMouseExit(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
