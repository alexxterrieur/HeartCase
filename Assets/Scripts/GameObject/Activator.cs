using System;
using System.Collections.Generic;
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
        if (HasConditions(itemsForHide))
        {
            if(ToggleActiveByConditions(itemsForHide, false, itemAlreadyPickedIndex))
            {
                return;
            }
        }

        if (HasConditions(dialoguesForShow) || ToggleActiveByConditions(dialoguesForShow, true, 0))
        {
            return;
        }

        gameObject.SetActive(false);
    }

    /// <summary>
    /// return true if all condition flags are checked
    /// </summary>
    /// <typeparam name="EnumType"> enum type</typeparam>
    /// <param name="currentEnum"> current enum you want to check </param>
    /// <param name="activate"> you want to activate or desactivate </param>
    /// <returns> return true if job is done </returns>
    private bool ToggleActiveByConditions<EnumType>(EnumType currentEnum, bool activate, int boolIndexInGameState) where EnumType : Enum
    {
        int totalFlagsActive = CountActiveFlags(currentEnum);
        int goodFlags = 0;
        foreach (EnumType flag in Enum.GetValues(typeof(EnumType)))
        {
            if (currentEnum.HasFlag(flag) && GameState.Instance.GetBool(boolIndexInGameState, GetItemID(flag))) 
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

    private bool HasConditions<EnumType>(EnumType currentEnum) where EnumType : Enum
    {
        return !(itemsForHide == ItemsForHide.None);
    }

    private int GetItemID<EnumType>(EnumType flag) where EnumType : Enum
    {
        return Convert.ToInt32(flag) == 128 ? 0 : Convert.ToInt32(flag);
    }

    private int CountActiveFlags<EnumType>(EnumType currentEnum) where EnumType : Enum
    {
        if (!Enum.IsDefined(typeof(EnumType), 0))
        {
            throw new ArgumentException($"L'énumération {typeof(EnumType).Name} doit avoir une valeur 'None' égale à 0.");
        }

        EnumType noneValue = (EnumType)Enum.ToObject(typeof(EnumType), 0);

        if (EqualityComparer<EnumType>.Default.Equals(currentEnum, noneValue))
        {
            return 0;
        }

        int count = 0;
        foreach (EnumType flag in Enum.GetValues(typeof(EnumType)))
        {
            if (!EqualityComparer<EnumType>.Default.Equals(flag, noneValue) && itemsForHide.HasFlag(flag))
            {
                count++;
            }
        }
        return count;
    }
}

[Flags]
public enum ItemsForHide
{
    None = 0,
    Debt = 1,
    Necklace = 2,
    Diary = 4,
    Key = 128,
}

[Flags]
public enum DialoguesForShow
{
    None = 0,
    Dialogue2 = 1,
    Dialogue3 = 2,
    Dialogue4 = 4,
    Dialogue1 = 128,
}
