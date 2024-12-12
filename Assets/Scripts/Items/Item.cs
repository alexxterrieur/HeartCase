using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : ScriptableObject, IItem
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _itemID;
    public string itemDescription;

    public string itemName { get => _itemName; set => _itemName = value; }
    public Sprite visual { get => _sprite; set => _sprite = value; }
    public int itemID { get => _itemID; set => _itemID = value; }
}
