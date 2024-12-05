using UnityEngine.UI;

public class ItemSlotView
{
    private Image itemImage;
    private ItemSlotData data;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="data">item slot data</param>
    /// <param name="itemImage">image that contain the item slot</param>
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
