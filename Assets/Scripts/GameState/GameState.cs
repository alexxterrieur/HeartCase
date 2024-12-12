using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private int boolsNumber;

    public static GameState Instance { get; private set; }

    private List<byte> boolsList = new List<byte>();
    //0 = dialogues, 1 = time, 2 = objectAlreadyPicked, 3 = CinematicsAlreadyDone

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            for(int i = 0; i < boolsNumber; i++)
            {
                boolsList.Add(0);
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Get boolean
    /// </summary>
    /// <param name="boolsIndex"> index of the byte you want </param>
    /// <param name="Id"> id of the item / the character ...</param>
    /// <returns></returns>
    public bool GetBool(int boolsIndex, int Id)
    {
        return hasOption(boolsIndex, Id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val">state you want to set the boolean</param>
    /// <param name="boolsIndex"> index of the boolean you want </param>
    /// <param name="Id"> id of the item / the character ... </param>
    public void SetBool(bool val, int boolsIndex, int Id)
    {
        if (val)
        {
            addOption(boolsIndex, Id);
            return;
        }

        removeOption(boolsIndex, Id);
    }

    public void TestSetBool(int id)
    {
        SetBool(true, 2, id);
    }

    public void DebugShowEveryBools()
    {
        for (int i = 0; i < boolsNumber; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                print("Index : " + i + " ID : " + j + " Value : " + GetBool(i, j));
            }
        }
    }

    void addOption(int boolsIndex, int Id)
    {
        boolsList[boolsIndex] |= (byte)(1 << Id);
    }

    void removeOption(int boolsIndex, int Id)
    {
        boolsList[boolsIndex] &= (byte)(~(1 << Id));
    }

    bool hasOption(int boolsIndex, int Id)
    {
        return (boolsList[boolsIndex] & (1 << Id)) > 0;
    }
}