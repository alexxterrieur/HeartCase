using UnityEngine;

public class PickUpObjectInteraction : Interactions
{
    [SerializeField] private ItemInspector itemInspector;
    [SerializeField] private Item itemGived;
   
    public override void Interact()
    {
        itemInspector.InspectItem(itemGived);
        print("interact");
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }
}
