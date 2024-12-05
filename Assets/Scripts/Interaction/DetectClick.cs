using UnityEngine;

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

    private void OnMouseUp()
    {
        if(!isOnObject) { return; }

        interaction.Interact();
    }
}
