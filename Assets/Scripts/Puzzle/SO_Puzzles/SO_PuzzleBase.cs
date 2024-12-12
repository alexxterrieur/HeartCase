using UnityEngine;

public class SO_PuzzleBase : ScriptableObject
{
    public string description; 
    public Sprite background;
    public GameObject puzzlePrefab;
    public float timePenalty = 60f;
    public SO_Reward reward;
}
