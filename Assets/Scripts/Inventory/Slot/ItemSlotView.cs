using TMPro;
using UnityEngine.UI;

public class ItemSlotView
{
    private Image itemImage;
    private ItemSlotData data;


    public ItemSlotView(ItemSlotData data, Image itemImage)
    {
        this.itemImage = itemImage;
        this.data = data;
    }

    public void Update()
    {
        itemImage.sprite = data.GetItemContained().visual;
    }
}
