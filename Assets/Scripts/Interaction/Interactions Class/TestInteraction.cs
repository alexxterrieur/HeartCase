public class TestInteraction : Interactions
{
    public override void Interact()
    {
        print("interact");
    }

    public override bool InteractWithItem(Item itemGived)
    {
        if(itemGived != itemNeeded) { return false; }

        print("interact with item");
        return true;
    }
}
