using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> optionsButtons;
    [SerializeField] private GameObject characters;
    [SerializeField] private GameObject changeScene;
    [HideInInspector] public bool changeSceneActive = false;

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

    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Replic CurrentReplic;

    private Replic.Postion lastPostion = Replic.Postion.Neutral;

    //Puzzle to start
    [Header("Puzzle")]
    [SerializeField] private UIFadeInFadeOut fade;
    [SerializeField] private PuzzleHandler puzzleHandler;
    private SO_PuzzleBase puzzle;

    [Header("Reward")]
    private SO_Reward rewardGived;
    private RewardGiver rewardGiver;

    private void Start()
    {
        rewardGiver = GetComponent<RewardGiver>();
    }

    public void StartDialogue(Dialogue displayedDialogue)
    {
        if (displayedDialogue == null)
        {
            Debug.LogWarning("Invalide dialogue -> check the dialogue");
            return;
        }

        if (characters != null)
        {
            characters.SetActive(false);
        }

        if (changeSceneActive)
        {
            changeScene.SetActive(false);
        }

        currentDisplayedDialogue = displayedDialogue;

        puzzle = currentDisplayedDialogue.puzzle;
        rewardGived = currentDisplayedDialogue.reward;

        currentDisplayedReplic = currentDisplayedDialogue.dialogue;
        SetDialogueUIActive(true);
        InitializeInterlocutorsSprite();
        CurrentReplic = currentDisplayedDialogue.dialogue;
        SetReplic();
    }

    private void InitializeInterlocutorsSprite()
    {
        if (currentDisplayedDialogue.rightInterlocutorSprite == null)
        {
            rightInterlocutorImage.gameObject.SetActive(false);
        }
        else
        {
            rightInterlocutorImage.gameObject.SetActive(true);
            rightInterlocutorImage.sprite = currentDisplayedDialogue.rightInterlocutorSprite;
        }

        if (currentDisplayedDialogue.leftInterlocutorSprite == null)
        {
            leftInterlocutorImage.gameObject.SetActive(false);
        }
        else
        {
            leftInterlocutorImage.gameObject.SetActive(true);
            leftInterlocutorImage.sprite = currentDisplayedDialogue.leftInterlocutorSprite;
        }
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
            return;
        }

        int currentOptionsActivate = 0;

        if (replic.possibleNextReply.Count == 1 && replic.possibleNextReply[0].reply == "Next")
        {
            optionsButtons[0].SetActive(true);
        }

        for (int i = 0; i < replic.possibleNextReply.Count; i++)
        {
            if (i == 0 && (replic.possibleNextReply[i].reply == "Next" || replic.possibleNextReply[i].reply == "Finish"))
            {
                optionsButtons[0].SetActive(true);
                continue;
            }

            if (!CanActivateThisReply(replic.possibleNextReply[i])) { continue; }
            optionsButtons[i + 1].SetActive(true);
            optionsButtons[i + 1].GetComponentInChildren<TextMeshProUGUI>().text = replic.possibleNextReply[i].reply;
            currentOptionsActivate++;
        }

        if (currentOptionsActivate > 0) { return; }

        optionsButtons[0].SetActive(true);
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
        if (choice < currentDisplayedReplic.possibleNextReply.Count && currentDisplayedReplic.possibleNextReply[choice].nextReplic != null)
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
            if (characters != null)
            {
                characters.SetActive(true);
            }

            if (changeSceneActive)
            {
                changeScene.SetActive(true);
            }

            return;
        }

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
                    //rightInterlocutorImage.sprite = currentReplic.rightCharacterSprite;
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
                    //leftInterlocutorImage.sprite = currentReplic.leftCharacterSprite;
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
            print(currentDisplayedReplic);
            print(currentDisplayedReplic.reward);
        SetDialogueUIActive(false);

        if (characters != null)
        {
            characters.SetActive(true);
        }

        if (changeSceneActive)
        {
            changeScene.SetActive(true);
        }

        if (rewardGived != null)
        {
            rewardGiver.GiveReward(rewardGived);
        }
        if (currentDisplayedReplic.reward != null)
        {
            rewardGiver.GiveReward(currentDisplayedReplic.reward);
        }
        if (puzzle != null)
        {
            fade.CallFade(StartPuzzle, puzzle);
        }
    }

    private void StartPuzzle(SO_PuzzleBase puzzle)
    {
        puzzleHandler.StartPuzzle(puzzle);
    }
}