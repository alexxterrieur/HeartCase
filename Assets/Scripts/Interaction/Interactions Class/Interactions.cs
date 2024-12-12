using UnityEngine;

public abstract class Interactions : MonoBehaviour
{
    [SerializeField] protected Item itemNeeded;

    public abstract void Interact();

    public abstract bool InteractWithItem(Item itemGived);
}
