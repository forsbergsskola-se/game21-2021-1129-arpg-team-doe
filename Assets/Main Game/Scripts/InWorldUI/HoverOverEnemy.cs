using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOverEnemy : MonoBehaviour
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D onEnemyCursor;

    void OnMouseEnter(){
        Cursor.SetCursor(onEnemyCursor, Vector2.zero , CursorMode.ForceSoftware);
    }

    void OnMouseExit(){
        Cursor.SetCursor(defaultCursor, Vector2.zero , CursorMode.ForceSoftware);
    }
}
