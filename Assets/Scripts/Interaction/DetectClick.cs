using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Interactions))]
public class DetectClick : MonoBehaviour
{
    private bool isOnObject;
    private Interactions interaction;

    public void SetIsOnObject(bool value) { isOnObject = value; }

    private void Awake()
    {
        interaction = GetComponent<Interactions>();
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    
    private void OnMouseUp()
    {
        if(!isOnObject || IsPointerOverUI()) { return; }

        interaction.Interact();
    }
}
