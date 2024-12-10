using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationInteraction : Interactions
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<UnityEvent> onAnimationFinished = new();
    private int currentAnimation = 0;
    private bool interactable = true;
    
    public override void Interact()
    {
        if (animator != null && interactable)
        {
            animator.SetTrigger("Interact");
        }
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }

    public void DeactivateInteraction()
    {
        interactable = false;
    }

    public void ActivateInteraction()
    {
        interactable = true;
    }

    public void AnimationFinished()
    {
        if (onAnimationFinished == null || onAnimationFinished.Count == 0) return;
        onAnimationFinished[currentAnimation].Invoke();
        
        currentAnimation++;
        if (currentAnimation >= onAnimationFinished.Count)
        {
            currentAnimation = 0;
        }
    }
}
