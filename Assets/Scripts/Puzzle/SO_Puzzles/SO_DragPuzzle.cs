using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DragPuzzle", menuName = "ScriptableObjects/Puzzle/DragPuzzle", order = 1)]
public class SO_DragPuzzle : SO_PuzzleBase
{
    public List<Sprite> objectsSprites = new List<Sprite>();
}
