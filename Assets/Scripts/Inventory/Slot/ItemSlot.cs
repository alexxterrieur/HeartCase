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
    public int Getnumber() { return data.GetNumber(); }

    public void AddItem(Item item, int number)
    {
        if(data.TryAddItem(item, number))
        {
            view.Update();
        }
    }

    public bool HasItem()
    {
        return data.GetItemContained() != data.GetDefaultItem();
    }
}
