using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CinematicHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UIFadeInFadeOut uiFadeInFadeOut;
    
    [SerializeField] private List<Sprite> firstCinematicSprites;
    [SerializeField] private List<Sprite> secondCinematicSprites;
    
    private Image imageRenderer;
    private GameObject parentGameObject;
    
    private bool fading;
    
    [SerializeField] private float maxCooldown = 3f;
    private float cooldown;
    public int currentCinematicIndex = 1;
    private int currentSpriteIndex;

    public void StartCinematic(int _cinematicIndex = 1)
    {
        currentCinematicIndex = _cinematicIndex;
        parentGameObject.SetActive(true);
        imageRenderer.enabled = false;
    }
    
    private void Awake()
    {
        imageRenderer = GetComponent<Image>();
        parentGameObject = transform.parent.gameObject;
        parentGameObject.SetActive(false);
    }

    private void Update()
    {
        if (fading) return;
        
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            StartCoroutine(WaitForFade());
        }
    }

    //Waits for the screen to Fade In before changing the sprite
    private IEnumerator WaitForFade()
    {
        fading = true;
        if (uiFadeInFadeOut != null)
        {
            uiFadeInFadeOut.CallFade();
        }
        yield return new WaitForSeconds(1);
        fading = false;
        ChangeSprite();
    }

    //Changes the sprite, if its the first sprite enable the image, if it's the last, disable image and gameObject
    private void ChangeSprite()
    {
        if (currentSpriteIndex == 0)
        {
            imageRenderer.enabled = true;
        }

        switch (currentCinematicIndex)
        {
            case 1:
                if (currentSpriteIndex >= firstCinematicSprites.Count)
                {
                    ResetSelf();
                    return;
                }
                imageRenderer.sprite = firstCinematicSprites[currentSpriteIndex];
                break;
            case 2:
                if (currentSpriteIndex >= secondCinematicSprites.Count)
                {
                    ResetSelf();
                    return;
                }
                imageRenderer.sprite = secondCinematicSprites[currentSpriteIndex];
                break;
            default:
                if (currentSpriteIndex >= firstCinematicSprites.Count)
                {
                    ResetSelf();
                    return;
                }
                imageRenderer.sprite = firstCinematicSprites[currentSpriteIndex];
                break;
        }

        currentSpriteIndex++;
        cooldown = maxCooldown + 1f;
    }

    private void ResetSelf()
    {
        parentGameObject.SetActive(false);
        currentSpriteIndex = 0;
        cooldown = 0;
    }
    
    //If the player clicks, skip the cooldown
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(WaitForFade());
    }
}
