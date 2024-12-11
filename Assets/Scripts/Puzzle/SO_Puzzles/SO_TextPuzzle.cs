using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ClickPuzzle", menuName = "ScriptableObjects/Puzzle/ClickPuzzle", order = 1)]
public class SO_TextPuzzle : SO_PuzzleBase
{
    public string answerText;
}
