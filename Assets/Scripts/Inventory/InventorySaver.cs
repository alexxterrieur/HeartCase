using System.Collections.Generic;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    public static InventorySaver Instance;

    private List<Item> items = new List<Item>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestorInventory()
    {
        foreach(Item item in items)
        {
            Inventory.Instance.RestorItem(item);
        }
    }

    public void AddSavedItem(Item item)
    {
        items.Add(item);
    }

    public void DeleteSavedObject(Item item)
    {
        items.Remove(item);
    }
}
