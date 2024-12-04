using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private int boolsNumber;

    public static GameState Instance { get; private set; }

    private List<byte> boolsList = new List<byte>();
    //0 = objects, 1 = time

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            for(int i = 0; i < boolsNumber; i++)
            {
                boolsList.Add(0);
            }
        }
    }

    public void CheckList()
    {
        for(int i = 0; i < boolsList.Count;i++)
        {
            print("index " + i);
            print("byte " + boolsList[i]);
        }
    }

    public bool GetBool(int boolsIndex, int Id)
    {
        return hasOption(boolsIndex, Id);
    }

    public void SetBool(bool val, int boolsIndex, int Id)
    {
        if (val)
        {
            print("add");
            addOption(boolsIndex, Id);
            return;
        }

        print("remove");
        removeOption(boolsIndex, Id);
    }

    void addOption(int boolsIndex, int Id)
    {
        print("params" + boolsIndex + " " + Id);
        print("avant " + boolsList[boolsIndex]);
        boolsList[boolsIndex] |= (byte)(1 << Id);
        print("après " + boolsList[boolsIndex]);
    }

    void removeOption(int boolsIndex, int Id)
    {
        print("params" + boolsIndex + " " + Id);
        print("avant " + boolsList[boolsIndex]);
        boolsList[boolsIndex] &= (byte)(~(1 << Id));
        print("après " + boolsList[boolsIndex]);
    }

    bool hasOption(int boolsIndex, int Id)
    {
        print("params" + boolsIndex + " " +  Id);
        print("byte " + boolsList[boolsIndex]);
        print("bit recherché " + ((1 << Id)));
        return (boolsList[boolsIndex] & (1 << Id)) > 0;
    }
}