public class ItemSlotData
{
    private Item itemContained;
    private Item defaultItem;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="defaultItem"> the default item ( item when the slot is empty )</param>
    public ItemSlotData(Item defaultItem)
    {
        itemContained = defaultItem;
        this.defaultItem = defaultItem;
    }

    public Item GetItemContained() { return itemContained; }
    public Item GetDefaultItem() {  return defaultItem; }

    /// <summary>
    /// try to add item and return true if succeeds
    /// </summary>
    /// <param name="item"> item to add </param>
    /// <returns> return true if succeeds </returns>
    public bool TryAddItem(Item item)
    {
        if (!IsEmpty()) { return false; }

        AddItem(item);
        return true;
    }

    /// <summary>
    /// add the item in the slot
    /// </summary>
    /// <param name="item"> item to add </param>
    private void AddItem(Item item)
    {
        if (IsEmpty())
        {
            itemContained = item;
        }
    }

    private bool IsValidItem(Item item)
    {
        return itemContained.itemName == item.itemName;
    }

    public bool IsEmpty()
    {
        return itemContained == defaultItem;
    }

    public void Reset()
    {
        itemContained = defaultItem;
    }
}
