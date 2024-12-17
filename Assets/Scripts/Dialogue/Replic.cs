using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Replic : ScriptableObject
{
    [Header("Characters Sprites")]
    public Sprite leftCharacterSprite;
    public Sprite rightCharacterSprite;
    
    [Header("Speaker character Properties")]
    public string characterName;
    public Postion characterPostion;
    
    [Header("Dialogue Properties")]
    public string frenchDialogueText;
    public string englishDialogueText;
    public List<Reply> possibleNextReply = new();
    
    
    public enum Postion
    {
        Neutral,
        Right,
        Left
    }
}
