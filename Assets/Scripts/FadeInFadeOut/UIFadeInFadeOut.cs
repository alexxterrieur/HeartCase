using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeInFadeOut : MonoBehaviour
{
    [SerializeField] private Image uiImage;
    [SerializeField] private float time;

    public void CallFade(Action<SO_Puzzle> startEnigm, SO_Puzzle puzzle)
    {
        StartCoroutine(Fade(time, 50, startEnigm, puzzle));
    }

    public void CallFade()
    {
        StartCoroutine(Fade(time, 50));
    }
    
    private IEnumerator Fade(float time, int steps, Action<SO_Puzzle> startEnigm = null, SO_Puzzle puzzle = null)
    {
        uiImage.raycastTarget = true;
        float ratio = (255f / (float)steps) / 100f;
        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, ratio);
            yield return new WaitForSeconds(time / steps);
        }

        if (startEnigm != null && puzzle is not null)
        {
            startEnigm(puzzle);
        }
        
        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, -ratio);
            yield return new WaitForSeconds(time / steps);
        }
        uiImage.raycastTarget = false;
    }

    private void AddAlpha(Image image, float alphaToAdd)
    {
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + alphaToAdd);
    }
}
