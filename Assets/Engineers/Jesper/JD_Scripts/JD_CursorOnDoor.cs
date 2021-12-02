using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JD_CursorOnDoor : MonoBehaviour
{
    internal bool unOpenable = true; //Why not call it unopenable?

    public Texture2D lockedTexture;
    public Texture2D unLockedTexture;
    public Texture2D cursor;

    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter(){
        if (!unOpenable){
            Cursor.SetCursor(unLockedTexture, new Vector2(), CursorMode.Auto);
        }
        if (unOpenable){
            Cursor.SetCursor(lockedTexture, new Vector2(), CursorMode.Auto);
        }
    }

    void OnMouseExit(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
