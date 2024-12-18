using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPuzzle : Puzzle
{
    [SerializeField] private GameObject objectsToIgnore;
    [SerializeField] private GameObject objectifGameObject;
    private GameObject lastObjectClicked = null;
    
    protected override bool CheckAnswer()
    {
        if (puzzle is SO_ClickPuzzle clickPuzzle)
        {
            return lastObjectClicked == objectifGameObject;
        }
        throw new ArgumentException("Invalid puzzle type");
    }
    
    protected override bool IsAnswerValid()
    {
        return lastObjectClicked != null;
    }
    
    public override void TryToSolve()
    {
        if (!IsAnswerValid())
        {
            Debug.Log("Please enter an answer");
            return;
        }
        
        if (CheckAnswer())
        {
            Debug.Log("You answered the puzzle");
            
            if(!rewardGiver) 
            {
            }
            else
            {
                rewardGiver.GiveReward(puzzle.rewards);
                onPuzzleSolved();
            }
            
            //WIN SOUND HERE
            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
            
            //LOSE SOUND HERE
            
            Destroy(gameObject);
        }
    }

    public override void OnPointerClick(PointerEventData _eventData)
    {
        if (informationPanel.activeInHierarchy) //if the info prompt is active and you click, it deactivate
        {
            informationPanel.SetActive(false);
            return;
        }

        if (puzzle is not SO_ClickPuzzle) throw new ArgumentException("Invalid puzzle type");

        //Behavior of the Click Puzzle from here

        GameObject clickedObject = _eventData.pointerCurrentRaycast.gameObject;

        if (clickedObject == null || clickedObject == gameObject ||
            clickedObject.transform.IsChildOf(objectsToIgnore.transform)) return;

        lastObjectClicked = clickedObject;
        TryToSolve();
    }
}
