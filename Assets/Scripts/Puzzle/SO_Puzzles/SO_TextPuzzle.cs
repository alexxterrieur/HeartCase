using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TextPuzzle", menuName = "ScriptableObjects/Puzzle/TextPuzzle", order = 1)]
public class SO_TextPuzzle : SO_PuzzleBase
{
    public string answerText;
}
