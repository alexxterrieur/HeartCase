using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite rightInterlocutorSprite;
    public Sprite leftInterlocutorSprite;
    
    public Replic dialogue;

    public List<Condition> conditions = new();

    public SO_PuzzleBase puzzle;
    public SO_Reward reward;
}
