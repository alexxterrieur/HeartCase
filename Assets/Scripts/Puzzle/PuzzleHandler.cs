using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private GameObject infoButton;
    [SerializeField] private Image guessedPositionImage;
    [SerializeField] private GameObject informationPanel;
    [SerializeField] private TextMeshProUGUI informationPromptText;
    [SerializeField] private Image backgroundImage;
    
    [SerializeField] private UnityEvent<float> onPuzzleFail;

    [Header("Reward")]
    [SerializeField] private SO_Reward reward;
    private RewardGiver rewardGiver;
    
    private Vector2 guessedPosition = Vector2.zero;

    public void StartPuzzle(SO_Puzzle _puzzle)
    {
        rewardGiver = GetComponent<RewardGiver>();
        puzzle = _puzzle;
        PuzzleSetActive();
    }
    
    private void PuzzleSetActive(bool isActive = true)
    {
        switch (puzzle.type)
        {
            case PuzzleType.TextPuzzle: //enables text input and submit button
                inputField.gameObject.SetActive(isActive);
                submitButtonTransform.gameObject.SetActive(isActive);
                break;
            case PuzzleType.ClickPuzzle: //enables and centers the submit button
                submitButtonTransform.gameObject.SetActive(isActive);
                submitButtonTransform.anchoredPosition = Vector2.up * submitButtonTransform.anchoredPosition.y;
                break;
            default:
                throw new Exception("Invalid puzzle type");
        }
        
        backgroundImage.sprite = puzzle.background;
        backgroundImage.enabled = isActive;
        
        infoButton.SetActive(isActive);
        
        informationPromptText.text = puzzle.description;
        InformationSetActive(isActive);
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
                Debug.Log(puzzle.answerPosition);
                Debug.Log(guessedPositionImage.transform.position);
                Debug.Log(puzzle.answerPosition - guessedPosition);
                Debug.Log((puzzle.answerPosition - guessedPosition).magnitude);
                return (puzzle.answerPosition - guessedPosition).magnitude <= puzzle.answerRange;
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
                guessedPositionImage.color = Color.green;
            }

            if(!rewardGiver) 
            {
                print("RewardGiver is null, no reward will be gived");
            }
            else
            {
                rewardGiver.GiveReward(reward);
            }

            PuzzleSetActive(false);
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
            if (puzzle.type == PuzzleType.ClickPuzzle)
            {
                guessedPositionImage.color = Color.red;
            }
            
            onPuzzleFail.Invoke(puzzle.timePenalty);
        }
    }

    public void OnPointerClick(PointerEventData _)
    {
        if (informationPanel.activeInHierarchy) //if the info prompt is active and you click, it deactivate
        {
            informationPanel.SetActive(false);
            return;
        }
        
        if (puzzle.type != PuzzleType.ClickPuzzle) return;
        
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

    public void InformationSetActive(bool isActive = true)
    {
        guessedPosition = Vector2.zero;
        guessedPositionImage.rectTransform.anchoredPosition = Vector2.zero;
        guessedPositionImage.gameObject.SetActive(false);
        informationPanel.SetActive(isActive);
    }

    /// <summary>
    /// Dev Tool to set the answer to the current guessed answer
    /// </summary>
    public void SetAsAnswer()
    {
        switch (puzzle.type)
        {
            case PuzzleType.TextPuzzle:
                puzzle.answerText = inputField.text;
                break;
            case PuzzleType.ClickPuzzle:
                puzzle.answerPosition = guessedPosition;
                break;
            default:
                throw new Exception("Invalid puzzle type");
        }
    }
}
