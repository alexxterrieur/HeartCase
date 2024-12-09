using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
[DisallowMultipleComponent]
public class DropDownController : MonoBehaviour, IPointerClickHandler
{
    public List<int> indexesToDisable = new List<int>();

    private Dropdown dropDown;

    private void Awake()
    {
        dropDown = GetComponent<Dropdown>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var dropDownList = GetComponentInChildren<Canvas>();
        if (!dropDownList) return;

        var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);

        for (var i = 1; i < toogles.Length; i++)
        {
            toogles[i].interactable = !indexesToDisable.Contains(i - 1);
        }
    }

    /// <summary>
    /// Anytime change a value by index
    /// </summary>
    /// <param name="index"> index of the option</param>
    /// <param name="enable"> True = active, False = desactive </param>
    public void EnableOption(int index, bool enable)
    {
        if (index < 1 || index > dropDown.options.Count)
        {
            Debug.LogWarning("Index out of range -> ignored!", this);
            return;
        }

        if (enable)
        {
            if (indexesToDisable.Contains(index)) indexesToDisable.Remove(index);
        }
        else
        {
            if (!indexesToDisable.Contains(index)) indexesToDisable.Add(index);
        }

        var dropDownList = GetComponentInChildren<Canvas>();

        if (!dropDownList) return;

        var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);
        toogles[index].interactable = enable;
    }
}
