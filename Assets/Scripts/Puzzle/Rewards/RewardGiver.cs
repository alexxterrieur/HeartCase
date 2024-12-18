using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RewardGiver : MonoBehaviour
{
    [SerializeField] private DropDownController dropDownController;
    [SerializeField] private DialogueManager dialogueManager;

    private void Start()
    {
        if(dialogueManager == null)
        {
            dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        }
    }

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
            if (rewardSO.boolIndex == 1 && dropDownController)
            {
                dropDownController.AddOptionByIndex(rewardSO.boolId);
                return;
            }
            GameState.Instance.SetBool(true, rewardSO.boolIndex, rewardSO.boolId);
        }

        if (rewardSO.dialogue.dialogue != null)
        {

#if UNITY_EDITOR
            Assert.IsNotNull(dialogueManager, "dialogueManager is null");
#endif
            dialogueManager.StartDialogue(rewardSO.dialogue);
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
