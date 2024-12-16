using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SO_PuzzleBase : ScriptableObject
{
    public string description; 
    public Sprite background;
    public GameObject puzzlePrefab;
    public float timePenalty = 60f;
    public List<SO_Reward> rewards;
}
