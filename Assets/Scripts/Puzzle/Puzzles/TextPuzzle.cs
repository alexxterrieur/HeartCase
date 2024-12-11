using System;
using TMPro;
using UnityEngine;

public class TextPuzzle : Puzzle
{
    [SerializeField] private TMP_InputField inputField;
    
    protected override bool CheckAnswer()
    {
        if (puzzle is SO_TextPuzzle textPuzzle)
        {
            return TransformText(inputField.text) == textPuzzle.answerText;
        }
        throw new ArgumentException("Invalid puzzle type");
    }

    /// <summary>
    /// Modify the text to avoid typos (uppercase instead of lowercase, too many space ...)
    /// </summary>
    private string TransformText(string _text)
    {
        return _text.ToLower();
    }
    
    protected override bool IsAnswerValid()
    {
        return inputField.text != "";
    }

    public override void SetAsAnswer()
    {
        if (puzzle is not SO_TextPuzzle textPuzzle) throw new ArgumentException("Invalid puzzle type");
        textPuzzle.answerText = TransformText(inputField.text);
    }
}
