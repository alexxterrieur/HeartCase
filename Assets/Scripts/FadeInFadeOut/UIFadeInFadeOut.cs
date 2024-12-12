using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFadeInFadeOut : MonoBehaviour
{
    [SerializeField] private Image uiImage;
    [SerializeField] private float time;

    public void CallFade(Action<Item> startEnigm, Item puzzleBase)
    {
        StartCoroutine(Fade(time, 50, startEnigm, puzzleBase));
    }

    public void CallFade(Action<SO_PuzzleBase> startEnigm, SO_PuzzleBase puzzleBase)
    {
        StartCoroutine(Fade(time, 50, startEnigm, puzzleBase));
    }

    public void CallFade()
    {
        StartCoroutine(Fade(time, 50));
    }
    
    private IEnumerator Fade(float time, int steps, Action<Item> giveItem = null, Item item = null)
    {
        uiImage.raycastTarget = true;
        float ratio = (255f / (float)steps) / 100f;
        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, ratio);
            yield return new WaitForSeconds(time / steps);
        }

        if (giveItem != null || item is not null)
        {
           giveItem.Invoke(item);
        }
        
        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, -ratio);
            yield return new WaitForSeconds(time / steps);
        }
        uiImage.raycastTarget = false;
    }

    private IEnumerator Fade(float time, int steps, Action<SO_PuzzleBase> startEnigm = null, SO_PuzzleBase puzzleBase = null)
    {
        uiImage.raycastTarget = true;
        float ratio = (255f / (float)steps) / 100f;
        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, ratio);
            yield return new WaitForSeconds(time / steps);
        }

        if (startEnigm != null || puzzleBase is not null)
        {
            startEnigm.Invoke(puzzleBase);
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, -ratio);
            yield return new WaitForSeconds(time / steps);
        }
        uiImage.raycastTarget = false;
    }

    private IEnumerator Fade(float time, int steps)
    {
        uiImage.raycastTarget = true;
        float ratio = (255f / (float)steps) / 100f;
        for (int i = 0; i < steps; i++)
        {
            AddAlpha(uiImage, ratio);
            yield return new WaitForSeconds(time / steps);
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
