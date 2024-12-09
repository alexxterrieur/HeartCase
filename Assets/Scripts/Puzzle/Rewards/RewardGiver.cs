using UnityEngine;

public class RewardGiver : MonoBehaviour
{
    public void GiveReward(SO_Reward rewardSO)
    {
        if (rewardSO == null)
        {
            throw new System.Exception("The reward Scriptable object is null, no reward can be gived");
        }

        if (rewardSO.reward)
        {
            Inventory.Instance.AddItem(rewardSO.reward);
            GameState.Instance.SetBool(true, 2, rewardSO.reward.itemID);
        }

        if (rewardSO.boolId >= 0 && rewardSO.boolIndex >= 0)
        {
            GameState.Instance.SetBool(true, rewardSO.boolIndex, rewardSO.boolId);
        }
    }
}
