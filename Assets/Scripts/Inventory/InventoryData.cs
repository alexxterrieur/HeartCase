using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public GameObject slot;
    private Transform _transform;

    private List<ItemSlot> inventoryItemContainer = new();
    private List<GameObject> inventorySlots = new();

    public InventoryData(GameObject slot, Transform _transform)
    {
        this.slot = slot;
        this._transform = _transform;
    }

    public List<ItemSlot> GetItemContainerList() { return inventoryItemContainer; }
    public List<GameObject> GetSlotList() { return inventorySlots; }

    public void Initialize(int numberOfSlots)
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject newSlot = MonoBehaviour.Instantiate(slot, _transform);
            inventoryItemContainer.Add(newSlot.GetComponent<ItemSlot>());
        }
    }
}
