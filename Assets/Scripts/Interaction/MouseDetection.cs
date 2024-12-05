using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    [SerializeField] private Texture2D cursorOnOver;
    private CursorMode cursorMode = CursorMode.Auto;


    public void OnMouseEnter()
    {
        print("entre");
        Cursor.SetCursor(cursorOnOver, Vector2.zero, cursorMode);
    }

    public void OnMouseExit()
    {
        print("sort");
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
