using UnityEngine;

public class CursorOnDoor : MonoBehaviour{
    [SerializeField] Texture2D lockedCursor;
    [SerializeField] Texture2D unLockedCursor;
    [SerializeField] Texture2D cursor;
    internal bool openable;

    void Start(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter(){
        if (!openable){
            Cursor.SetCursor(lockedCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        if (openable){
            Cursor.SetCursor(unLockedCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    void OnMouseExit(){
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
