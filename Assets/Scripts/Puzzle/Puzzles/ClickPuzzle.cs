using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickPuzzle : Puzzle
{
    [SerializeField] private Image guessedPositionImage;
    private Vector2 guessedPosition = Vector2.zero;
    
    protected override bool CheckAnswer()
    {
        if (puzzle is SO_ClickPuzzle clickPuzzle)
        {
            return (clickPuzzle.answerPosition - guessedPosition).magnitude <= clickPuzzle.answerRange;
        }
        throw new ArgumentException("Invalid puzzle type");
    }
    
    protected override bool IsAnswerValid()
    {
        return guessedPosition != Vector2.zero;
    }

    public override void SetAsAnswer()
    {
        if (puzzle is not SO_ClickPuzzle clickPuzzle) throw new ArgumentException("Invalid puzzle type");
        clickPuzzle.answerPosition = guessedPosition;
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

            guessedPositionImage.color = Color.green;
            
            if(!rewardGiver) 
            {
                print("RewardGiver is null, no rewards will be gived");
            }
            else
            {
                rewardGiver.GiveReward(puzzle.rewards);
                onPuzzleSolved();
            }
            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
            
            guessedPositionImage.color = Color.red;
        }
    }
    
    public override void OnPointerClick(PointerEventData _)
    {
        base.OnPointerClick(_);
        
        if (puzzle is not SO_ClickPuzzle) throw new ArgumentException("Invalid puzzle type");
        
        //Behavior of the Click Puzzle from here
        
        guessedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (guessedPositionImage == null) return;
        
        if (guessedPosition == Vector2.zero)
        {
            guessedPositionImage.gameObject.SetActive(false);
            return;
        }
        
        guessedPositionImage.gameObject.SetActive(true);
        guessedPositionImage.color = Color.white;
        guessedPositionImage.rectTransform.anchoredPosition = Input.mousePosition;
    }
    
    public override void InformationSetActive(bool isActive = true)
    {
        base.InformationSetActive(isActive);
        guessedPosition = Vector2.zero;
        guessedPositionImage.rectTransform.anchoredPosition = Vector2.zero;
        guessedPositionImage.gameObject.SetActive(false);
    }
}
