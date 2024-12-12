using System.Collections.Generic;
using UnityEngine;

public class SceneActivatorManager : MonoBehaviour
{
    [SerializeField] private List<Activator> interactibleObjects = new List<Activator>();

    public void CheckActivateAndDesactivate()
    {
        for (int i = 0; i < interactibleObjects.Count; ++i)
        {
            interactibleObjects[i].ActiveOrDesactiveGO();
        }
    }
}
