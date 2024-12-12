using UnityEngine;

public class PuzzleInteractions : Interactions
{
    [SerializeField] private PuzzleHandler puzzleHandler;
    [SerializeField] private SO_PuzzleBase puzzle;
    
    public override void Interact()
    {
        if (itemNeeded == null)
        {
            puzzleHandler.StartPuzzle(puzzle);
        }
    }

    public override bool InteractWithItem(Item itemGived)
    {
        if (itemGived != itemNeeded) return false;
        puzzleHandler.StartPuzzle(puzzle);
        return true;
    }
}
