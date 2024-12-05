public class ItemSlotData
{
    private Item itemContained;
    private int itemNumber;
    private Item defaultItem;

    public ItemSlotData(Item defaultItem)
    {
        itemContained = defaultItem;
        this.defaultItem = defaultItem;
    }

    public int GetNumber() {  return itemNumber; }

    public Item GetItemContained() { return itemContained; }
    public Item GetDefaultItem() {  return defaultItem; }

    public bool TryAddItem(Item item, int number = 1)
    {
        if (!IsEmpty()) { return false; }

        AddItem(item, number);
        return true;
    }

    private void AddItem(Item item, int number)
    {
        if (IsEmpty())
        {
            itemContained = item;
            itemNumber = number;
        }
        else { itemNumber += number; }
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
        itemNumber = 0;
    }
}
