using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Puzzle : MonoBehaviour, IPointerClickHandler
{
    public Action onPuzzleSolved;
    public Action onPuzzleFail;
    
    public SO_PuzzleBase puzzle;
    
    [Header("UI Elements")]
    [SerializeField] protected GameObject informationPanel;
    [SerializeField] protected TextMeshProUGUI informationPromptText;
    [SerializeField] protected Image backgroundImage;
    
    [Header("Reward")]
    protected RewardGiver rewardGiver;

    protected virtual void Start()
    {
        rewardGiver = GetComponent<RewardGiver>();
        InitPuzzleUI();
    }

    protected virtual void InitPuzzleUI()
    {
        backgroundImage.sprite = puzzle.background;
        informationPromptText.text = puzzle.description;
    }
    
    protected abstract bool CheckAnswer();

    /// <summary>
    /// Checks if the answer is valid
    /// </summary>
    protected abstract bool IsAnswerValid();

    public virtual void TryToSolve()
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
                print("RewardGiver is null, no reward will be gived");
            }
            else
            {
                rewardGiver.GiveReward(puzzle.reward);
                onPuzzleSolved();
            }
            
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Puzzle could not be solved");
            
            onPuzzleFail();
        }
    }

    /// <summary>
    /// Dev Tool to set the answer to the current guessed answer
    /// </summary>
    public virtual void SetAsAnswer() { }
    
    public virtual void InformationSetActive(bool isActive = true)
    {
        informationPanel.SetActive(isActive);
    }
    
    public virtual void OnPointerClick(PointerEventData _)
    {
        if (informationPanel.activeInHierarchy) //if the info prompt is active and you click, it deactivate
        {
            informationPanel.SetActive(false);
        }
    }
}
