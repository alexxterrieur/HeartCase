using System.Collections.Generic;
using UnityEngine;

public class RewardGiver : MonoBehaviour
{
    [SerializeField] private DropDownController DropDownController;

    public void GiveReward(SO_Reward rewardSO)
    {
        if (rewardSO == null)
        {
            throw new System.Exception("The rewards Scriptable object is null, no rewards can be gived");
        }

        if (rewardSO.reward)
        {
            Inventory.Instance.AddItem(rewardSO.reward);
            GameState.Instance.SetBool(true, 2, rewardSO.reward.itemID);
        }

        if (rewardSO.boolId >= 0 && rewardSO.boolIndex >= 0)
        {
            if (rewardSO.boolIndex == 1 && DropDownController)
            {
                DropDownController.AddOptionByIndex(rewardSO.boolId);
                return;
            }
            GameState.Instance.SetBool(true, rewardSO.boolIndex, rewardSO.boolId);
        }
    }

    public void GiveReward(List<SO_Reward> _rewards)
    {
        foreach (SO_Reward reward in _rewards)
        {
            GiveReward(reward);
        }
    }
}
