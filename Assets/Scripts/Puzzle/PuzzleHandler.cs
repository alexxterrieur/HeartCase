using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PuzzleHandler : MonoBehaviour
{
    [FormerlySerializedAs("puzzle")] [SerializeField] private SO_PuzzleBase puzzleBase;

    [Header("SceneManager")]
    [SerializeField] private SceneActivatorManager sceneActivatorManager;

    public void StartPuzzle(SO_PuzzleBase puzzleBase)
    {
        this.puzzleBase = puzzleBase;
        
        //Instantiate the new puzzleBase from SO Here (and associate Events)
        Puzzle puzzle = Instantiate(puzzleBase.puzzlePrefab, transform).GetComponent<Puzzle>();
        
        puzzle.puzzle = puzzleBase;
        puzzle.onPuzzleSolved = PuzzleSuccess;
    }

    private void PuzzleSuccess()
    {
        sceneActivatorManager.CheckActivateAndDesactivate();
    }

    /*
    /// <summary>
    /// Verifies the answer inputted by the player depending on the type of puzzleBase
    /// </summary>
    private bool CheckAnswer()
    {
        switch (puzzleBase.type)
        {
            case PuzzleType.ClickPuzzle:
                return TransformText(inputField.text) == puzzleBase.answerText;
            case PuzzleType.ClickPuzzle:
                return (puzzleBase.answerPosition - guessedPosition).magnitude <= puzzleBase.answerRange;
            default:
                throw new Exception("Invalid puzzleBase type");
        }
    }
    */
    
    /*
    /// <summary>
    /// Checks if the answer is valid (exemple : if the position of the guessedAnswer is exactly the center, then it's not valid)
    /// </summary>
    private bool IsAnswerValid()
    {
        switch (puzzleBase.type)
        {
            case PuzzleType.ClickPuzzle:
                return inputField.text != "";
            case PuzzleType.ClickPuzzle:
                return guessedPosition != Vector2.zero;
            default:
                throw new Exception("Invalid puzzleBase type");
        }
    }
    */
    
    /*
    public void TryToSolve()
    {
        if (!IsAnswerValid())
        {
            Debug.Log("Please enter an answer");
            return;
        }
        
        if (CheckAnswer())
        {
            Debug.Log("You answered the puzzleBase");
            if (puzzleBase.type == PuzzleType.ClickPuzzle)
            {
                guessedPositionImage.color = Color.green;
            }

            if(!rewardGiver) 
            {
                print("RewardGiver is null, no reward will be gived");
            }
            else
            {
                rewardGiver.GiveReward(reward);
                sceneActivatorManager.CheckActivateAndDesactivate();
            }

            //PuzzleSetActive(false);
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
            if (puzzleBase.type == PuzzleType.ClickPuzzle)
            {
                guessedPositionImage.color = Color.red;
            }
            
            onPuzzleFail.Invoke(puzzleBase.timePenalty);
        }
    }
    */
    
    
}
