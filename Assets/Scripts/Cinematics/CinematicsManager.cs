using System.Collections.Generic;
using UnityEngine;

public class CinematicsManager : MonoBehaviour
{
    [SerializeField] private List<Cinematic> cinematics = new List<Cinematic>();
    
    private void Start()
    {
        StartCinematic(0);
    }

    public void StartCinematic(int cinematicIndex)
    {
        if (GameState.Instance.GetBool(3, cinematicIndex) || cinematics.Count <= cinematicIndex) return;
        
        cinematics[cinematicIndex].StartCinematic();
        GameState.Instance.SetBool(true, 3, cinematicIndex);
    }
}
