using UnityEngine;

public class PickUpObjectInteraction : Interactions
{
    [SerializeField] private ItemInspector itemInspector;
    [SerializeField] private Item itemGived;

    public override void Interact()
    {
        print("interact");
        itemInspector.InspectItem(itemGived);
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }
}
