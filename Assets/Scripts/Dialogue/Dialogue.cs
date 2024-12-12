using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite rightInterlocutorSprite;
    public Sprite leftInterlocutorSprite;
    
    public Replic dialogue;

    public List<ConditionForDialogue> conditions = new();
}
