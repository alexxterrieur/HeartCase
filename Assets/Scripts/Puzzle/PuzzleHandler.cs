using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PuzzleType
{
    TextPuzzle,
    ClickPuzzle
}

public class PuzzleHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SO_Puzzle puzzle;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private RectTransform submitButtonTransform;
    [SerializeField] private Image image;
    
    private Vector2 guessedPosition = Vector2.zero;

    private void Start()
    {
        InitPuzzleUI();
    }

    private void InitPuzzleUI()
    {
        switch (puzzle.type)
        {
            case PuzzleType.TextPuzzle: //enables text input and submit button
                inputField.gameObject.SetActive(true);
                submitButtonTransform.gameObject.SetActive(true);
                break;
            case PuzzleType.ClickPuzzle: //enables and centers the submit button
                submitButtonTransform.gameObject.SetActive(true);
                submitButtonTransform.anchoredPosition = Vector2.up * submitButtonTransform.anchoredPosition.y;
                break;
            default:
                throw new Exception("Invalid puzzle type");
        }
    }


    /// <summary>
    /// Verifies the answer inputted by the player depending on the type of puzzle
    /// </summary>
    private bool CheckAnswer()
    {
        switch (puzzle.type)
        {
            case PuzzleType.TextPuzzle:
                return TransformText(inputField.text) == puzzle.answerText;
            case PuzzleType.ClickPuzzle:
                return (guessedPosition - puzzle.answerPosition).magnitude <= puzzle.answerRange;
            default:
                throw new Exception("Invalid puzzle type");
        }
    }

    /// <summary>
    /// Checks if the answer is valid (exemple : if the position of the guessedAnswer is exactly the center, then it's not valid)
    /// </summary>
    private bool IsAnswerValid()
    {
        switch (puzzle.type)
        {
            case PuzzleType.TextPuzzle:
                return inputField.text != "";
            case PuzzleType.ClickPuzzle:
                return guessedPosition != Vector2.zero;
            default:
                throw new Exception("Invalid puzzle type");
        }
    }
    
    /// <summary>
    /// Modify the text to avoid typos (uppercase instead of lowercase, too many space ...)
    /// </summary>
    private string TransformText(string _text)
    {
        
        return _text.ToLower();
    }

    public void TryToSolve()
    {
        if (!IsAnswerValid())
        {
            Debug.Log("Please enter an answer");
            return;
        }
        
        if (CheckAnswer())
        {
            Debug.Log("You answered the puzzle");
            if (puzzle.type == PuzzleType.ClickPuzzle)
            {
                image.color = Color.green;
            }
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
            if (puzzle.type == PuzzleType.ClickPuzzle)
            {
                image.color = Color.red;
            }
        }
    }

    public void OnPointerClick(PointerEventData _)
    {
        if (puzzle.type != PuzzleType.ClickPuzzle) return;
        
        guessedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (image == null) return;
        
        if (guessedPosition == Vector2.zero)
        {
            image.gameObject.SetActive(false);
            return;
        }
        
        image.gameObject.SetActive(true);
        image.color = Color.white;
        image.rectTransform.anchoredPosition = Input.mousePosition;
    }
}
