using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<Dialogues> dialogues = new List<Dialogues>();
    
    //Interlocutor
    [Header("Interlocutors")]
    [SerializeField] private Image rightInterlocutorImage; 
    [SerializeField] private Image leftInterlocutorImage;
    
    [SerializeField] private float scaleMultiplier;
    private Vector3 originalRightInterlocutorScale;
    private Vector3 originalLeftInterlocutorScale;
    
    [Space(10f)]
    
    //Dialogue
    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private LanguageManager languageManager;

    private TMP_Text characterNameText;
    private TMP_Text dialogueText;
    
    private int currentDialogueIndex = 0;
    private List<Dialogue> currentDialogues;

    private Dialogue.Postion lastPostion = Dialogue.Postion.Neutral;

    //Puzzle to start
    [Header("Puzzle")]
    [SerializeField] private UIFadeInFadeOut fade;
    [SerializeField] private PuzzleHandler puzzleToStart;
    [FormerlySerializedAs("soPuzzle")] [SerializeField] private SO_PuzzleBase soPuzzleBase;

    private void Start()
    {
        characterNameText = dialogueBox.transform.GetChild(1).GetComponent<TMP_Text>();
        dialogueText = dialogueBox.transform.GetChild(2).GetComponent<TMP_Text>();
    }

    public void StartDialogue()
    {
        if (dialogues == null || dialogues.Count == 0)
        {
            return;
        }

        //Check dialogue conditions

        SwitchActiveDialogue();
        currentDialogues = dialogues[0].dialogues;
        DisplayDialogue(currentDialogues, currentDialogueIndex);
    }

    private void SwitchActiveDialogue()
    {
        rightInterlocutorImage.gameObject.SetActive(!rightInterlocutorImage.gameObject.activeSelf);
        leftInterlocutorImage.gameObject.SetActive(!leftInterlocutorImage.gameObject.activeSelf);
        
        dialogueBox.gameObject.SetActive(!dialogueBox.gameObject.activeSelf);
    }

    public void DisplayNextDialogue()
    {
        if (currentDialogues == null || currentDialogues.Count == 0)
        {
            Debug.Log("No Dialogue Found");
            return;
        }
        
        DisplayDialogue(currentDialogues, currentDialogueIndex);
    }

    private void DisplayDialogue(List<Dialogue> _currentDialogues, int _index)
    {
        //Check dialogue count
        if (_index <= _currentDialogues.Count - 1)
        {
            SetDialogue(_currentDialogues[_index]);
            currentDialogueIndex++;
        }
        else
        {
            CallAction();
            currentDialogueIndex = 0;
            currentDialogues = null;
        }
    }
    
    private void SetDialogue(Dialogue _currentDialogue)
    {
        SetDialogueBox(_currentDialogue);
        SetDialogueBoxLanguage(_currentDialogue);
    }

    private void SetDialogueBox(Dialogue _currentDialogue)
    {
        characterNameText.text = _currentDialogue.characterName;
        Debug.Log($"Current character position: {_currentDialogue.characterPostion}");
        
        //Set Position
        switch (_currentDialogue.characterPostion)
        {
            case Dialogue.Postion.Right:
                if (lastPostion == Dialogue.Postion.Right)
                {
                    rightInterlocutorImage.sprite = _currentDialogue.rightCharacterSprite;
                }
                else if (lastPostion == Dialogue.Postion.Neutral)
                {
                    SpeakInterlocutor(rightInterlocutorImage);
                    leftInterlocutorImage.color = Color.grey;
                }
                else
                {
                    SetInterlocutors(rightInterlocutorImage, leftInterlocutorImage);
                }
                lastPostion = Dialogue.Postion.Right;
                break;
            case Dialogue.Postion.Left:
                if (lastPostion == Dialogue.Postion.Left)
                {
                    leftInterlocutorImage.sprite = _currentDialogue.leftCharacterSprite;
                }
                else if (lastPostion == Dialogue.Postion.Neutral)
                {
                    SpeakInterlocutor(leftInterlocutorImage);
                    rightInterlocutorImage.color = Color.grey;
                }
                else
                {
                    SetInterlocutors(leftInterlocutorImage, rightInterlocutorImage);
                }
                lastPostion = Dialogue.Postion.Left;
                break;
        }
    }

    private void SetDialogueBoxLanguage(Dialogue _currentDialogue)
    {
        //Set language
        switch (languageManager.language)
        {
            case LanguageManager.Languages.English:
                dialogueText.text = _currentDialogue.englishDialogueText;
                break;
            case LanguageManager.Languages.French:
                dialogueText.text = _currentDialogue.frenchDialogueText;
                break;
        }
    }
    
    private void SpeakInterlocutor(Image _interlocutorImageToSpeak)
    {
        _interlocutorImageToSpeak.color = Color.white;
        _interlocutorImageToSpeak.transform.localScale *= scaleMultiplier;
    }
    
    private void HideInterlocutor(Image _interlocutorImageToHide)
    {
        _interlocutorImageToHide.color = Color.grey;
        _interlocutorImageToHide.transform.localScale /= scaleMultiplier;
    }
    
    private void SetInterlocutors(Image _interlocutorImageToSpeak, Image _interlocutorImageToHide)
    {
        SpeakInterlocutor(_interlocutorImageToSpeak);
        HideInterlocutor(_interlocutorImageToHide);
    }

    private void CallAction()
    {
        SwitchActiveDialogue();
        fade.CallFade(puzzleToStart.StartPuzzle, soPuzzleBase);
        Debug.Log("End Dialogue");
    }
}