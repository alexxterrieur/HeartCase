using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StartDialogueInteractionOnStart : Interactions
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueManager dialogueManager;

    private void Start()
    {
        Interact();
    }

    public override void Interact()
    {
        dialogueManager.StartDialogue(dialogue);
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }
}
