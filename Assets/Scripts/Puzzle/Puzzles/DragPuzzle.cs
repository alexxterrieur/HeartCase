using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPuzzle : Puzzle, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Puzzle Elements")]
    [SerializeField] private GameObject objectifObject;
    
    private GameObject currentObject;
    private bool isDraging = false;
    
    protected override bool CheckAnswer()
    {
        return currentObject == objectifObject;
    }

    protected override bool IsAnswerValid()
    {
        return true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraging || currentObject == null) return;
        currentObject.transform.position += (Vector3)eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerCurrentRaycast.gameObject;
        if (draggedObject == gameObject || draggedObject.GetComponent<TextMeshProUGUI>() != null) return;
        ChangeCurrentObjectUI(draggedObject);
        currentObject = draggedObject;
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        if (isDraging) return;
        
        GameObject draggedObject = eventData.pointerCurrentRaycast.gameObject;
        if (draggedObject == gameObject || draggedObject.GetComponent<TextMeshProUGUI>() != null) return;
        ChangeCurrentObjectUI(draggedObject);
        currentObject = draggedObject;
    }

    private void ChangeCurrentObjectUI(GameObject newObject)
    {
        if (currentObject != null && currentObject.TryGetComponent(out Image image))
        {
            Color color = image.color;
            image.color = new Color(color.r, color.g, color.b, 1f);
        }

        if (newObject == null || !newObject.TryGetComponent(out Image newImage)) return;
        Color newColor = newImage.color;
        newImage.color = new Color(newColor.r, newColor.g, newColor.b, 0.5f);
    }
}
