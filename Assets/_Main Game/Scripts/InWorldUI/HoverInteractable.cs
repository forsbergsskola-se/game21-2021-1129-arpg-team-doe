using UnityEngine;

public class HoverInteractable : MonoBehaviour{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D interactableObject;

    void OnMouseEnter(){
        Cursor.SetCursor(interactableObject, Vector2.zero , CursorMode.ForceSoftware);
    }

    void OnMouseExit(){
        Cursor.SetCursor(defaultCursor, Vector2.zero , CursorMode.ForceSoftware);
    }
}
