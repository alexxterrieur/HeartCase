using UnityEngine;

public class Activator : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void ActiveOrDesactiveGO()
    {

    }
}

[System.Flags]
public enum ItemsForHide
{
    None = 128,
    Key = 0,
    Debt = 1,
    Necklace = 2,
    Diary = 4,
}

[System.Flags]
public enum DialoguesForShow
{
    None = 128,
    Dialogue1 = 0,
    Dialogue2 = 1,
    Dialogue3 = 2,
    Dialogue4 = 4,
}
