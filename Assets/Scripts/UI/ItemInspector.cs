using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{
    [SerializeField] private GameObject itemInspectorGameObject;
    [SerializeField] private GameObject restOfUI;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private Item itemInspected;

    public void InspectItem(Item item)
    {
        itemInspectorGameObject.SetActive(true);
        restOfUI.SetActive(false);

        itemDescription.text = item.itemDescription;
        itemImage.sprite = item.visual;
        itemInspected = item;
    }

    public void PickItem()
    {
        Inventory.Instance.AddItem(itemInspected);
        GameState.Instance.SetBool(true, 2, itemInspected.itemID);

        itemInspectorGameObject.SetActive(false);
        restOfUI.SetActive(true);
    }

}
