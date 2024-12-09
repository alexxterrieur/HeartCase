using UnityEngine;

public class RewardGiver : MonoBehaviour
{
    public void GiveReward(SO_Reward rewardSO)
    {
        if(rewardSO == null)
        {
            throw new System.Exception("The reward Scriptable object is null, no reward can be gived");
        }

        if (rewardSO.reward)
        {

        }
    }
}
