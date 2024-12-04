using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SO_Puzzle puzzle;
    [SerializeField] private TMP_InputField inputField;

    private bool CheckAnswer()
    {
        if (puzzle.isTextPuzzle)
        {
            return TransformText(inputField.text) == puzzle.answerText;
        }

        return ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - puzzle.answerPosition).magnitude <= puzzle.answerRange;
    }

    private string TransformText(string _text)
    {
        return _text.ToLower();
    }

    private void TryToSolve()
    {
        if (CheckAnswer())
        {
            Debug.Log("You answered the puzzle");
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
        }
    }

    public void OnEndEdit()
    {
        if (!puzzle.isTextPuzzle) return;
        TryToSolve();
    }

    public void OnPointerClick(PointerEventData _)
    {
        if (puzzle.isTextPuzzle) return;
        TryToSolve();
    }
}
