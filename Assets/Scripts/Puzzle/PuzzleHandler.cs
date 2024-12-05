using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SO_Puzzle puzzle;
    [SerializeField] private TMP_InputField inputField;
    
    private Vector2 guessedPosition = Vector2.zero;

    private bool CheckAnswer()
    {
        if (puzzle.isTextPuzzle)
        {
            return TransformText(inputField.text) == puzzle.answerText;
        }

        if (guessedPosition == Vector2.zero)
        {
            return false;
        }
        
        return (guessedPosition - puzzle.answerPosition).magnitude <= puzzle.answerRange;
    }

    private string TransformText(string _text)
    {
        return _text.ToLower();
    }

    public void TryToSolve()
    {
        if (guessedPosition == Vector2.zero && inputField.text == "")
        {
            Debug.Log("Please enter an answer");
            return;
        }
        
        if (CheckAnswer())
        {
            Debug.Log("You answered the puzzle");
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
        }
    }

    public void OnPointerClick(PointerEventData _)
    {
        if (!puzzle.isTextPuzzle)
        {
            guessedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
