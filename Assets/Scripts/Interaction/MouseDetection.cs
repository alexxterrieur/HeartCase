using UnityEngine;

[RequireComponent(typeof(DetectClick))]
public class MouseDetection : MonoBehaviour
{
    [SerializeField] private Texture2D cursorOnOver;
    private CursorMode cursorMode = CursorMode.Auto;
    private DetectClick interaction;

    private void Awake()
    {
        interaction = GetComponent<DetectClick>();
    }

    public void OnMouseOver()
    {
        if (interaction.IsPointerOverUI()) return;
        Cursor.SetCursor(cursorOnOver, Vector2.zero, cursorMode);
        interaction.SetIsOnObject(true);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        interaction.SetIsOnObject(false);
    }
}
