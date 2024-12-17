using System.Collections.Generic;
using UnityEngine;

public class PickUpObjectInteraction : Interactions
{
    [SerializeField] private ItemInspector itemInspector;
    [SerializeField] private Item itemGived;
    [SerializeField] private List<SO_Reward> possibleRewards = new();
    [Header("serialize this only if you have some possibles rewards ^")]
    [SerializeField] private RewardGiver rewardGiver;

    public override void Interact()
    {
        itemInspector.InspectItem(itemGived);
        if(possibleRewards.Count > 0)
        {
            itemInspector.possibleRewards = possibleRewards;
            itemInspector.rewardGiver = rewardGiver;
        }
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }
}
