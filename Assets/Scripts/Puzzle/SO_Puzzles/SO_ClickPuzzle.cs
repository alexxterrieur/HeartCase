using UnityEngine;

[CreateAssetMenu(fileName = "ClickPuzzle", menuName = "ScriptableObjects/Puzzle/ClickPuzzle", order = 1)]
public class SO_ClickPuzzle : SO_PuzzleBase
{
    public Vector2 answerPosition = Vector2.zero;
    public float answerRange = 1;
}
