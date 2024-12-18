using System.Collections.Generic;
using UnityEngine;

public class SceneActivatorManager : MonoBehaviour
{
    [SerializeField] private List<Activator> interactibleObjects = new List<Activator>();

    private void Start()
    {
        GameState.Instance.OnStateChange.AddListener(ActivateOrDesactivate);
    }

    public void ActivateOrDesactivate()
    {
        print("cherche");
        foreach (Activator activator in interactibleObjects)
        {
            activator.ActiveOrDesactiveGO();
        }
    }
}
