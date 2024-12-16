using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Item defaultItem;

    private ItemSlotData data;
    private ItemSlotView view;

    private void Awake()
    {
        data = new ItemSlotData(defaultItem);
        view = new ItemSlotView(data, itemImage);
    }

    public void ResetItem()
    {
        data.Reset();
        view.Update();
    }

    public Item GetItem() { return data.GetItemContained(); }

    /// <summary>
    /// add item in the slot and update the image
    /// </summary>
    /// <param name="item"> item to add </param>
    public void AddItem(Item item)
    {
        if(data.TryAddItem(item))
        {
            view.Update();
        }
    }

    public void UpdateVisual()
    {
        view.Update();
    }

    public bool HasItem()
    {
        return data.GetItemContained() != data.GetDefaultItem();
    }
}
