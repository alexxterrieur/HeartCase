using UnityEngine;

[RequireComponent(typeof(DetectClick))]
public class MouseDetection : MonoBehaviour
{
    [SerializeField] private Texture2D cursorOnOver;
    private DetectClick interaction;
    private Sprite sprite;

    private void Awake()
    {
        interaction = GetComponent<DetectClick>();
        sprite = GetComponent<Sprite>();
    }

    public void OnMouseOver()
    {
        if (interaction.IsPointerOverUI()) return;
        Cursor.SetCursor(cursorOnOver, new Vector2(cursorOnOver.width, 0), CursorMode.ForceSoftware);
        interaction.SetIsOnObject(true);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        interaction.SetIsOnObject(false);
    }
}
