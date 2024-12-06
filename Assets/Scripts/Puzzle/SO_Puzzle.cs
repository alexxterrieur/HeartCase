using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "ScriptableObjects/Puzzle", order = 1)]
public class SO_Puzzle : ScriptableObject
{
    public string description; 
    public Sprite background;
    public PuzzleType type;
    
    public Vector2 answerPosition = Vector2.zero;
    public float answerRange = 1;
    
    public string answerText;

    public float timePenalty = 60f;
}
