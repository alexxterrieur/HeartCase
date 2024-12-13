public class TestInteraction : Interactions
{

    public DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public override void Interact()
    {
        //dialogueManager.StartDialogue();
        print("interact");
    }

    public override bool InteractWithItem(Item itemGived)
    {
        if(itemGived != itemNeeded) { return false; }

        print("interact with item");
        return true;
    }
}
