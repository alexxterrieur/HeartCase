using UnityEngine;
using UnityEngine.Events;

public class AnimationInteraction : Interactions
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animation;
    
    public override void Interact()
    {
        if (animator != null)
        {
            animator.Play(animation.name);
        }
    }

    public override bool InteractWithItem(Item itemGived)
    {
        return false;
    }
}
