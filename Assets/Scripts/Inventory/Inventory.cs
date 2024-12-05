using System.Collections;
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
        StartCoroutine(WaitAndHide());
    }

    public void AddItem(Item item, int number)
    {
        ItemSlot itemSlot = data.GetItemContainerList()
            .Where(slot => !slot.HasItem())
            .FirstOrDefault();
        if (itemSlot != null)
        {
            itemSlot.AddItem(item, number);
        }
    }

    private void AddItemInSlot(ItemSlot slot, int number, Item item)
    {
        slot.AddItem(item, number);
    }

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


    public int GetNumberOfItem(Item item)
    {
        int number = 0;
        for (int i = 0; i < data.GetItemContainerList().Count(); i++)
        {
            if (data.GetItemContainerList()[i].GetItem() == item)
            {
                number += data.GetItemContainerList()[i].Getnumber();
            }
        }
        return number;
    }

    public void RemoveNumberOfItem(Item item, int number)
    {
        int numberRemaining = number;
        int numberRemoving = 0;
        for (int i = 0; i < data.GetItemContainerList().Count(); i++)
        {
            if(number == 0) { return; }

            if (data.GetItemContainerList()[i].GetItem() == item)
            {
                if(data.GetItemContainerList()[i].Getnumber() <= numberRemaining)
                {
                    numberRemaining -= data.GetItemContainerList()[i].Getnumber();
                    RemoveItemFromSlot(data.GetItemContainerList()[i]);
                    if (numberRemaining == 0) { return; }
                }
                else if(data.GetItemContainerList()[i].Getnumber() > numberRemaining)
                {
                    numberRemoving = numberRemaining;
                    numberRemaining -= data.GetItemContainerList()[i].Getnumber();
                    RemoveNumberOfItemFromSlot(data.GetItemContainerList()[i], numberRemoving);
                }
            }
        }
    }

    public void RemoveItemFromSlot(ItemSlot slot)
    {
        slot.ResetItem();
    }

    private void RemoveNumberOfItemFromSlot(ItemSlot slot, int number)
    {
        if(number > slot.Getnumber())
        {
            throw new System.Exception("$The number you want to remove from the slot is greater than the number in it : " + number + " > " + slot.Getnumber());
        }
        AddItemInSlot(slot, -number, slot.GetItem());
    }

    public void TestAddItem()
    {
        AddItem(testItem, 1);
    }

    public void TestAddItem2()
    {
        AddItem(testItem2, 1);
    }

    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(0.0001f);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
