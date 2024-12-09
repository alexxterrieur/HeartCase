using UnityEngine;

[CreateAssetMenu(fileName = "SO_Reward", menuName = "ScriptableObjects/SO_Reward")]
public class SO_Reward : ScriptableObject
{
    [Header("To add an object in the inventory")]
    public Item reward;

    [Header("To activate an interaction")]
    public int boolIndex;
    public int boolId;
}
