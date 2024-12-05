using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int numberOfSlots;

    [SerializeField] private GameObject slot;
    [SerializeField] private Transform slotsPanel;
    
    private InventoryData data;

    public Item testItem;
    public Item testItem2;

    void Start()
    {
        data = new(slot, slotsPanel);
        data.Initialize(numberOfSlots);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    /// <summary>
    /// add item in inventory
    /// </summary>
    /// <param name="item"> item to add in inventory </param>
    public void AddItem(Item item)
    {
        ItemSlot itemSlot = data.GetItemContainerList()
            .Where(slot => !slot.HasItem())
            .FirstOrDefault();
        if (itemSlot != null)
        {
            itemSlot.AddItem(item);
        }
    }

    /// <summary>
    /// add item in a specific slot
    /// </summary>
    /// <param name="slot"> slot that will reciev the item</param>
    /// <param name="item"> item to add in the slot </param>
    private void AddItemInSlot(ItemSlot slot, Item item)
    {
        slot.AddItem(item);
    }

    /// <summary>
    /// check in inventory if they are a specific item
    /// </summary>
    /// <param name="item"> item you want to know if is in inventory</param>
    /// <returns></returns>
    public bool HasTheItem(Item item)
    {
        for (int i = 0; i < data.GetItemContainerList().Count; i++)
        {
            if (data.GetItemContainerList()[i].GetItem() == item)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemFromSlot(ItemSlot slot)
    {
        slot.ResetItem();
    }

#if UNITY_EDITOR

    // test to add items

    public void TestAddItem()
    {
        AddItem(testItem);
    }

    public void TestAddItem2()
    {
        AddItem(testItem2);
    }

#endif
}
