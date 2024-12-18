using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour, IPointerClickHandler
{
    private Image uiImage;
    
    [SerializeField] private UnityEvent OnCinematicFinished;
    
    [SerializeField] private Image skipImage;
    
    [SerializeField] private List<Image> cinematicImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> cinematicTexts = new List<TextMeshProUGUI>();
    
    private bool cooldownActive = false;
    
    [SerializeField] private float maxCooldown = 2f;
    private float cooldown;
    private int currentIndex;
    
    private TextMeshProUGUI oldText = null;

    private void Awake()
    {
        uiImage = GetComponent<Image>();
        ResetSelf();
    }

    public void StartCinematic()
    {
        ResetSelf();
        uiImage.enabled = true;

        if (cinematicImages[currentIndex] is not null)
        {
            AddAlpha(cinematicImages[currentIndex], 1);
        }

        if (cinematicTexts[currentIndex] is not null)
        {
            oldText = cinematicTexts[currentIndex];
            AddAlpha(oldText, 1);
        }
        currentIndex++;
        
        cooldown = maxCooldown;
        cooldownActive = true;
    }
    
    private void Update()
    {
        if (!cooldownActive) return;
        
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            DisplayNextCinematicFrame();
        }
    }

    private void DisplayNextCinematicFrame()
    {
        cooldownActive = false;
        Color c = skipImage.color;
        skipImage.color = new Color(c.r, c.g, c.b, 0);
        cooldown = maxCooldown;
        
        StartCoroutine(FadeInNextFrame(1, 50));
    }

    private IEnumerator FadeInNextFrame(float time, int steps)
    {
        if (currentIndex >= cinematicImages.Count || currentIndex >= cinematicTexts.Count)
        {
            EndCinematic();
            yield break;
        }
        
        TextMeshProUGUI currentText = cinematicTexts[currentIndex];
        
        Image currentImage = cinematicImages[currentIndex];
        
        float ratio = (255f / (float)steps) / 100f;
        for (int i = 0; i < steps; i++)
        {
            if (currentText is not null && oldText is not null) AddAlpha(oldText, -ratio);
            
            yield return new WaitForSeconds(time / steps);
        }
        
        yield return new WaitForSeconds(0.25f);
        
        for (int i = 0; i < steps; i++)
        {
            if (currentText is not null) AddAlpha(currentText, ratio);
            
            if (currentImage is not null) AddAlpha(currentImage, ratio);
            
            yield return new WaitForSeconds(time / steps);
        }

        if (currentText is not null) oldText = currentText;
        
        currentIndex++;
        cooldown = maxCooldown;
        cooldownActive = true;
        Color c = skipImage.color;
        skipImage.color = new Color(c.r, c.g, c.b, 1);
    }
    
    private void AddAlpha(Image image, float alphaToAdd)
    {
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + alphaToAdd);
    }
    
    private void AddAlpha(TextMeshProUGUI text, float alphaToAdd)
    {
        Color currentColor = text.color;
        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + alphaToAdd);
    }

    private void EndCinematic()
    {
        cooldownActive = false;
        ResetSelf();
        OnCinematicFinished.Invoke();
    }
    
    private void ResetSelf()
    {
        currentIndex = 0;
        cooldown = maxCooldown;
        Color c = skipImage.color;
        skipImage.color = new Color(c.r, c.g, c.b, 0);

        foreach (Image image in cinematicImages)
        {
            if (image is not null)
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }

        foreach (TextMeshProUGUI text in cinematicTexts)
        {
            if (text is not null)
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
        
        uiImage.enabled = false;
    }
    
    //If the player clicks, skip the cooldown
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!cooldownActive) return;
        DisplayNextCinematicFrame();
    }
}
