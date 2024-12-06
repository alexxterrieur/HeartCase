using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameState : MonoBehaviour
{
    [SerializeField] private int boolsNumber;

    public static GameState Instance { get; private set; }

    private List<byte> boolsList = new List<byte>();
    //0 = objects, 1 = time, 2 objectAlreadyPicked

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