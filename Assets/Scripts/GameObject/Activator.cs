using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private List<Condition> forHide;
    [SerializeField] private List<Condition> forShow;

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
        if (forHide.Count > 0)
        {
            if (ToggleActiveByConditions(forHide, false))
            {
                return;
            }
        }

        if (forShow.Count > 0 || ToggleActiveByConditions(forShow, true))
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
    private bool ToggleActiveByConditions(List<Condition> conditions, bool activate)
    {
        int totalConditionsActive = conditions.Count;
        int goodConditions = 0;
        foreach (Condition condition in conditions)
        {
            if (GameState.Instance.GetBool(condition.boolListIndex, condition.boolId))
            {
                goodConditions++;
            }
        }

        if (goodConditions == totalConditionsActive)
        {
            gameObject.SetActive(activate);
            return true;
        }
        return false;
    }
}