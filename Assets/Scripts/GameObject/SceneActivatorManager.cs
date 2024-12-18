using System.Collections.Generic;
using UnityEngine;

public class SceneActivatorManager : MonoBehaviour
{
    [SerializeField] private List<Activator> interactibleObjects = new List<Activator>();

    private void Start()
    {
        GameState.Instance.OnStateChange.AddListener(ActivateOrDesactivate);

        ActivateOrDesactivate();
    }

    public void ActivateOrDesactivate()
    {
        foreach (Activator activator in interactibleObjects)
        {
            activator.ActiveOrDesactiveGO();
        }
    }
}
