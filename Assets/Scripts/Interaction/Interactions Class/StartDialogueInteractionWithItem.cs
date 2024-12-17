using System.Collections.Generic;
using UnityEngine;

public class StartDialogueInteractionWithItem : Interactions
{
    [SerializeField] private Dialogue dialogueWhenDontHaveItem;
    [SerializeField] private List<Dialogue> dialogues = new List<Dialogue>();
    [SerializeField] private DialogueManager dialogueManager;

    public override void Interact()
    {
        dialogueManager.StartDialogue(dialogueWhenDontHaveItem);
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
