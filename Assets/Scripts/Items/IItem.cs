using UnityEngine;

public interface IRenderable<VisualType>
{
    public VisualType visual { get; set; }
}

public interface IItem : IRenderable<Sprite>
{
    public string itemName { get; set; }
    public int itemID { get; set; }
}