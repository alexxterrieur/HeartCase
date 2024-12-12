using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> optionsButtons;
    private Dialogue currentDisplayedDialogue;
    private Replic currentDisplayedReplic;

    //Interlocutor
    [Header("Interlocutors")]
    [SerializeField] private Image rightInterlocutorImage;
    [SerializeField] private Image leftInterlocutorImage;

    [SerializeField] private float scaleMultiplier;

    [Space(10f)]

    //Dialogue
    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private LanguageManager languageManager;

    private TextMeshProUGUI characterNameText;
    private TextMeshProUGUI dialogueText;

    private Replic CurrentReplic;

    private Replic.Postion lastPostion = Replic.Postion.Neutral;

    //Puzzle to start
    [Header("Puzzle")]
    [SerializeField] private UIFadeInFadeOut fade;
    [SerializeField] private PuzzleHandler puzzleToStart;
    [FormerlySerializedAs("soPuzzle")] [SerializeField] private SO_PuzzleBase soPuzzleBase;

    private void Start()
    {
        characterNameText = dialogueBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        dialogueText = dialogueBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void StartDialogue(Dialogue displayedDialogue)
    {
        print("start");
        print("dialogue : " + displayedDialogue);
        if (displayedDialogue == null)
        {
            Debug.LogWarning("Invalide dialogue -> check the dialogue");
            return;
        }

        currentDisplayedDialogue = displayedDialogue;
        currentDisplayedReplic = currentDisplayedDialogue.dialogue;
        print(currentDisplayedReplic);
        SetDialogueUIActive(true);
        InitializeInterlocutorsSprite();
        CurrentReplic = currentDisplayedDialogue.dialogue;
        SetReplic();
    }

    private void InitializeInterlocutorsSprite()
    {
        rightInterlocutorImage.sprite = currentDisplayedDialogue.rightInterlocutorSprite;
        leftInterlocutorImage.sprite = currentDisplayedDialogue.leftInterlocutorSprite;
    }

    private void SetDialogueUIActive(bool active)
    {
        dialogueBox.gameObject.SetActive(active);
    }

    private void ActivateValidesOptions(Replic replic)
    {
        for (int i = 0; i < optionsButtons.Count; i++)
        {
            optionsButtons[i].SetActive(false);
        }

        if (replic.possibleNextReply.Count == 0)
        {
            optionsButtons[0].SetActive(true);
            optionsButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Finish";
            return;
        }

        int currentOptionsActivate = 0;

        for (int i = 0; i < replic.possibleNextReply.Count; i++)
        {
            if (!CanActivateThisReply(replic.possibleNextReply[i])) { continue; }

            optionsButtons[i].SetActive(true);
            optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = replic.possibleNextReply[i].reply;
            currentOptionsActivate++;
        }

        if (currentOptionsActivate > 0) { return; }

        optionsButtons[0].SetActive(true);
        optionsButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Finish";
        return;
    }

    private bool CanActivateThisReply(Reply reply)
    {
        if (reply.conditions.Count == 0) { return true; }

        for (int i = 0; i < reply.conditions.Count; i++)
        {
            if (!GameState.Instance.GetBool(reply.conditions[i].boolListIndex, reply.conditions[i].boolId)) { return false; }
        }

        return true;
    }

    public void DisplayReplic(int choice)
    {
        if (CurrentReplic == null)
        {
            Debug.Log("No Replic Found");
            return;
        }

        DisplayReplicByChoice(choice);
    }

    private void DisplayReplicByChoice(int choice)
    {
        //Check dialogue count
        if (choice < currentDisplayedReplic.possibleNextReply.Count)
        {
            currentDisplayedReplic = currentDisplayedReplic.possibleNextReply[choice].nextReplic;
            SetReplic();
        }
        else
        {
            CallAction();
        }
    }

    private void SetReplic()
    {
        if (currentDisplayedReplic == null)
        {
            CallAction();
            return;
        }
        print(currentDisplayedReplic);
        SetDialogueBox(currentDisplayedReplic);
        SetDialogueBoxLanguage(currentDisplayedReplic);
        ActivateValidesOptions(currentDisplayedReplic);
    }

    private void SetDialogueBox(Replic currentReplic)
    {
        characterNameText.text = currentReplic.characterName;

        //Set Position
        switch (currentReplic.characterPostion)
        {
            case Replic.Postion.Right:
                if (lastPostion == Replic.Postion.Right)
                {
                    rightInterlocutorImage.sprite = currentReplic.rightCharacterSprite;
                }
                else if (lastPostion == Replic.Postion.Neutral)
                {
                    SpeakInterlocutor(rightInterlocutorImage);
                    leftInterlocutorImage.color = Color.grey;
                }
                else
                {
                    SetSpeekingInterlocutor(rightInterlocutorImage, leftInterlocutorImage);
                }
                lastPostion = Replic.Postion.Right;
                break;

            case Replic.Postion.Left:
                if (lastPostion == Replic.Postion.Left)
                {
                    leftInterlocutorImage.sprite = currentReplic.leftCharacterSprite;
                }
                else if (lastPostion == Replic.Postion.Neutral)
                {
                    SpeakInterlocutor(leftInterlocutorImage);
                    rightInterlocutorImage.color = Color.grey;
                }
                else
                {
                    SetSpeekingInterlocutor(leftInterlocutorImage, rightInterlocutorImage);
                }
                lastPostion = Replic.Postion.Left;
                break;
        }
    }

    private void SetDialogueBoxLanguage(Replic _currentDialogue)
    {
        dialogueText.text = _currentDialogue.englishDialogueText;

        ////Set language
        //switch (languageManager.language)
        //{
        //    case LanguageManager.Languages.English:
        //        dialogueText.text = _currentDialogue.englishDialogueText;
        //        break;
        //    case LanguageManager.Languages.French:
        //        dialogueText.text = _currentDialogue.frenchDialogueText;
        //        break;
        //}
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

    private void SetSpeekingInterlocutor(Image _interlocutorImageToSpeak, Image _interlocutorImageToHide)
    {
        SpeakInterlocutor(_interlocutorImageToSpeak);
        HideInterlocutor(_interlocutorImageToHide);
    }

    private void CallAction()
    {
        Debug.Log("FINI Start Action");

        SetDialogueUIActive(false);
        if(soPuzzleBase != null)
        {
            fade.CallFade(puzzleToStart.StartPuzzle, soPuzzleBase);
        }
    }
}