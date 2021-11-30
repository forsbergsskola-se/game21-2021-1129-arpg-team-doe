using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorOnDoor : MonoBehaviour
{
    internal bool unOpenable = true;

    public Texture2D lockedTexture;
    public Texture2D unLockedTexture;
    public Texture2D cursor;

    private void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter()
    {
        if (!unOpenable)
        {
            Cursor.SetCursor(unLockedTexture, new Vector2(), CursorMode.Auto);
        }
        if (unOpenable)
        {
            Cursor.SetCursor(lockedTexture, new Vector2(), CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
