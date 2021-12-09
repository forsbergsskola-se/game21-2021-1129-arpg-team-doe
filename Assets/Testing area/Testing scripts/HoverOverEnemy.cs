using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOverEnemy : MonoBehaviour
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D onEnemyCursor;
    
    Vector2 offSet = new(15, 15);
    void OnMouseEnter(){
        Cursor.SetCursor(onEnemyCursor, offSet , CursorMode.ForceSoftware);
    }

    void OnMouseExit(){
        Cursor.SetCursor(defaultCursor, offSet , CursorMode.ForceSoftware);
    }
}
