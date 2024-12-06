using System;
using System.Linq;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private ItemsForHide itemsForHide;
    [SerializeField] private DialoguesForShow dialoguesForShow;

    [SerializeField] private int itemAlreadyPickedIndex = 2;

    private void Start()
    {
        ActiveOrDesactiveGO();
    }

    /// <summary>
    /// Active or desactive the gamobject
    /// </summary>
    public void ActiveOrDesactiveGO()
    {
        if (!itemsForHide.HasFlag(ItemsForHide.None))
        {
            if(ToggleActiveByConditions(itemsForHide, false))
            {
                return;
            }
        }

        if (dialoguesForShow.HasFlag(DialoguesForShow.None))
        {
            return;
        }

        if (ToggleActiveByConditions(dialoguesForShow, true))
        {
            return;
        }

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="EnumType"> enum type</typeparam>
    /// <param name="currentEnum"> current enum you want to check </param>
    /// <param name="activate"> you want to activate or desactivate </param>
    /// <returns> return true if job is done </returns>
    private bool ToggleActiveByConditions<EnumType>(EnumType currentEnum, bool activate) where EnumType : Enum
    {
        int totalFlagsActive = CountActiveFlags(itemsForHide);
        int goodFlags = 0;
        foreach (EnumType flag in Enum.GetValues(typeof(EnumType)))
        {
            if (itemsForHide.HasFlag(flag) && GameState.Instance.GetBool(itemAlreadyPickedIndex, Convert.ToInt32(flag))) 
            { 
                print(flag);
                goodFlags++;
            }
        }

        if (goodFlags == totalFlagsActive)
        {
            gameObject.SetActive(activate);
            return true;
        }
        return false;
    }

    int CountActiveFlags<EnumType>(EnumType flags) where EnumType : Enum
    {
        return Enum.GetValues(typeof(EnumType))
                   .Cast<EnumType>()
                   .Count(flag => flags.HasFlag(flag));
    }
}

[Flags]
public enum ItemsForHide
{
    None = 128,
    Key = 0,
    Debt = 1,
    Necklace = 2,
    Diary = 4,
}

[Flags]
public enum DialoguesForShow
{
    None = 128,
    Dialogue1 = 0,
    Dialogue2 = 1,
    Dialogue3 = 2,
    Dialogue4 = 4,
}
