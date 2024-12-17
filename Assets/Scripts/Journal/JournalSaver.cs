using System;
using System.Collections.Generic;
using UnityEngine;

public class JournalSaver : MonoBehaviour
{
    public static JournalSaver Instance;
    public List<string> texts = new List<string>();
    public int maxPage = 10;
    public int currentPage = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
