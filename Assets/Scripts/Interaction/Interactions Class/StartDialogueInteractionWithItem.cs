using System.Collections.Generic;
using UnityEngine;

public class StartDialogueInteractionWithItem : Interactions
{
    [SerializeField] private List<Dialogue> dialogueWhenDontHaveItem = new List<Dialogue>();
    [SerializeField] private List<Dialogue> dialogues = new List<Dialogue>();
    [SerializeField] private DialogueManager dialogueManager;

    public override void Interact()
    {
        int dialogueIndex = 0;
        foreach (Dialogue dialogue in dialogueWhenDontHaveItem)
        {
            int conditionsNumber = dialogue.conditions.Count;
            if (conditionsNumber == 0)
            {
                dialogueIndex = dialogueWhenDontHaveItem.IndexOf(dialogue);
                break;
            }

            int conditionsCheck = 0;
            foreach (Condition condition in dialogue.conditions)
            {
                if (!GameState.Instance.GetBool(condition.boolListIndex, condition.boolId))
                {
                    break;
                }
                conditionsCheck++;
            }

            if (conditionsCheck == conditionsNumber)
            {
                dialogueIndex = dialogueWhenDontHaveItem.IndexOf(dialogue);
                break;
            }
        }

        Dialogue displayedDialogue = dialogueWhenDontHaveItem[dialogueIndex];
        dialogueManager.StartDialogue(displayedDialogue);
    }

    public override bool InteractWithItem(Item itemGived)
    {
        if(itemGived != itemNeeded)
        {
            return false;
        }

        int dialogueIndex = 0;
        foreach (Dialogue dialogue in dialogues)
        {
            int conditionsNumber = dialogue.conditions.Count;
            if (conditionsNumber == 0)
            {
                dialogueIndex = dialogues.IndexOf(dialogue);
                break;
            }

            int conditionsCheck = 0;
            foreach (Condition condition in dialogue.conditions)
            {
                if (!GameState.Instance.GetBool(condition.boolListIndex, condition.boolId))
                {
                    break;
                }
                conditionsCheck++;
            }

            if (conditionsCheck == conditionsNumber)
            {
                dialogueIndex = dialogues.IndexOf(dialogue);
                break;
            }
        }

        Dialogue displayedDialogue = dialogues[dialogueIndex];
        dialogueManager.StartDialogue(displayedDialogue);
        return true;
    }
}
