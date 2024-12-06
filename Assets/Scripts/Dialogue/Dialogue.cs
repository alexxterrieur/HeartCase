using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    //Characters
    [Header("List Character")]
    public Sprite leftCharacterSprite;
    public Sprite rightCharacterSprite;
    
    //Character properties
    [Header("Character Properties")]
    public string characterName;
    public Postion characterPostion;
    
    //Dialogue properties
    [Header("Dialogue Properties")]
    public string frenchDialogueText;
    public string englishDialogueText;
    public List<byte> dialogueConditions;
    
    
    public enum Postion
    {
        Neutral,
        Right,
        Left
    }
    
    private enum Reward
    {
        Object
    }
}
