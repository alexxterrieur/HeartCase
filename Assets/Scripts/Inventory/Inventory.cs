using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int numberOfSlots;

    [SerializeField] private GameObject slot;
    [SerializeField] private Transform slotsPanel;
    
    private InventoryData data;

#if UNITY_EDITOR

    public Item testItem;
    public Item testItem2;

#endif

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        data = new(slot, slotsPanel);
        data.Initialize(numberOfSlots);
        transform.GetChild(0).gameObject.SetActive(false);

        InventorySaver.Instance.RestorInventory();
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
            InventorySaver.Instance.AddSavedItem(item);
        }
    }

    public void RemoveItem(Item item)
    {
        ItemSlot itemSlot = data.GetItemContainerList()
            .Where(slot => slot.HasItem() && slot.GetItem() == item)
            .FirstOrDefault();
        print(itemSlot);
        if (itemSlot != null)
        {
            itemSlot.ResetItem();
            InventorySaver.Instance.DeleteSavedObject(item);
        }
    }

    public void RestorItem(Item item)
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

    public void UpdateVisuals()
    {
        foreach(ItemSlot slot in data.GetItemContainerList())
        {
            slot.UpdateVisual();
        }
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
        GameState.Instance.SetBool(true, 2, testItem.itemID);
    }

    public void TestAddItem2()
    {
        AddItem(testItem2);
        GameState.Instance.SetBool(true, 2, testItem.itemID);
    }

#endif
}
