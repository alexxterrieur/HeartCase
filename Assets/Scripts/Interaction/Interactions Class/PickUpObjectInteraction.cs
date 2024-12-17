using UnityEngine;

public class PickUpObjectInteraction : Interactions
{
    [SerializeField] private ItemInspector itemInspector;
    [SerializeField] private Item itemGived;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemGived.visual;
    }

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
