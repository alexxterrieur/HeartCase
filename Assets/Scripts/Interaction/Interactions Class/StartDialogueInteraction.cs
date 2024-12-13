using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StartDialogueInteraction : Interactions
{
    [SerializeField] private List<Dialogue> dialogues = new List<Dialogue>();
    [SerializeField] private DialogueManager dialogueManager;

    public override void Interact()
    {
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
            foreach(ConditionForDialogue condition in dialogue.conditions) 
            {
                if (!GameState.Instance.GetBool(condition.boolListIndex, condition.boolId))
                {
                    break;
                }
                conditionsCheck++;
            }

            if(conditionsCheck == conditionsNumber)
            {
                dialogueIndex = dialogues.IndexOf(dialogue);
                break;
            }
        }

        print("interact");
        Dialogue displayedDialogue = dialogues[dialogueIndex];
        dialogueManager.StartDialogue(displayedDialogue);
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }
}
