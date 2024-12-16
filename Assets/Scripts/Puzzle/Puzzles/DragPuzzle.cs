using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPuzzle : Puzzle, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Puzzle Elements")]
    [SerializeField] private List<GameObject> objectifObjects = new();
    [SerializeField] private List<GameObject> objectifObjectsFound = new();
    
    private GameObject currentObject;
    private bool isDraging;

    protected override void Start()
    {
        base.Start();
        objectifObjectsFound = objectifObjects;
    }

    protected override void InitPuzzleUI()
    {
        base.InitPuzzleUI();
        if (puzzle is not SO_DragPuzzle dragPuzzle) return;
        for (int i = 0; i < objectifObjects.Count; i++)
        {
            if (i >= objectifObjects.Count || i >= dragPuzzle.objectsSprites.Count) continue;
            objectifObjects[i].GetComponent<Image>().sprite = dragPuzzle.objectsSprites[i];
            objectifObjects[i].GetComponent<Image>().SetNativeSize();
        }
    }
    
    public override void TryToSolve()
    {
        if (!CheckAnswer()) return;
        if(rewardGiver)
        {
            rewardGiver.GiveReward(puzzle.rewards);
            onPuzzleSolved();
        }
            
        Destroy(gameObject);
    }

    protected override bool CheckAnswer()
    {
        return objectifObjectsFound.Count == 0;
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

        if (objectifObjectsFound.Contains(draggedObject))
        {
            objectifObjectsFound.Remove(draggedObject);
            draggedObject.SetActive(false);
            TryToSolve();
            return;
        }
        
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
        
        if (objectifObjectsFound.Contains(draggedObject))
        {
            objectifObjectsFound.Remove(draggedObject);
            draggedObject.SetActive(false);
            TryToSolve();
            return;
        }
        
        currentObject = draggedObject;
    }
}
