using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{
    [SerializeField] private GameObject itemInspectorGameObject;
    [SerializeField] private GameObject restOfUI;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private List<GameObject> clickablesObjects = new List<GameObject>();

    [HideInInspector] public List<SO_Reward> possibleRewards = new();
    [HideInInspector] public RewardGiver rewardGiver;

    private Item itemInspected;

    public void InspectItem(Item item)
    {
        foreach(GameObject go in clickablesObjects)
        {
            go.SetActive(false);
        }

        itemInspectorGameObject.SetActive(true);
        restOfUI.SetActive(false);

        itemDescription.text = item.itemDescription;
        itemImage.sprite = item.visual;
        itemInspected = item;
    }

    public void PickItem()
    {
        foreach (GameObject go in clickablesObjects)
        {
            go.SetActive(true);
        }

        Inventory.Instance.AddItem(itemInspected);
        GameState.Instance.SetBool(true, 2, itemInspected.itemID);

        itemInspectorGameObject.SetActive(false);
        restOfUI.SetActive(true);

        if (possibleRewards.Count > 0)
        {
            rewardGiver.GiveReward(possibleRewards);
        }
    }

}
