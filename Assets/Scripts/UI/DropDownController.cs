using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
[DisallowMultipleComponent]
public class DropDownController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int SceneUnlockedIndex = 1;
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private PopupManager popupManager;
    
    [FormerlySerializedAs("indexesToDisable")] public List<string> namesToDisable = new List<string>();

    [SerializeField] List<string> scenesNames = new List<string>();
    [SerializeField] List<string> options = new List<string>();
    
    private TMP_Dropdown dropDown;

    private void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        if (GameState.Instance)
        {
            InitDropDown();
        }
    }

    private void InitDropDown()
    {
        for (int i = 0; i < scenesNames.Count; i++)
        {
            if (SceneManager.GetActiveScene().name == scenesNames[i] || !GameState.Instance.GetBool(SceneUnlockedIndex, i)) continue;
            AddOption(scenesNames[i], false, false);
        }

        if (options.Count == 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Canvas dropDownList = GetComponentInChildren<Canvas>();
        if (!dropDownList) return;

        Toggle[] toogles = dropDownList.GetComponentsInChildren<Toggle>(true);

        for (int i = 1; i < namesToDisable.Count; i++)
        {
            foreach (Toggle toggle in toogles.Where(toggle => toggle.name == namesToDisable[i]))
            {
                toggle.interactable = false;    
            }
        }
    }

    public void AddOption(string option, bool displayPopUp, bool gameStateCheck)
    {
        if (!scenesNames.Contains(option) || (gameStateCheck && GameState.Instance.GetBool(SceneUnlockedIndex, scenesNames.IndexOf(option)))) return;

        dropDown.AddOptions(new List<string> { option });
        options.Add(option);
        
        GameState.Instance.SetBool(true, SceneUnlockedIndex, scenesNames.IndexOf(option));
        
        if (options.Count == 1)
        {
            dialogueManager.changeSceneActive = true;
            transform.parent.gameObject.SetActive(true);
            dropDown.onValueChanged.Invoke(0);
        }
        
        if (displayPopUp)
        {
            //Add POPUP Here //
            popupManager.DisplayPopUp(/*option*/);
        }
    }
    
    //
    public void AddOption(string option)
    {
        AddOption(option, true, true);
    }
    //

    public void AddOptionByIndex(int index)
    {
        AddOption(scenesNames[index], true, true);
    }

    /// <summary>
    /// Anytime change a value by index
    /// </summary>
    /// <param name="name"> name of the option</param>
    /// <param name="enable"> True = active, False = desactive </param>
    public void EnableOption(string name, bool enable)
    {
        if (dropDown.options.All(x => x.text != name))
        {
            Debug.LogWarning("Option not in DropDown => Ignored !", this);
            return;
        }

        if (enable)
        {
            if (namesToDisable.Contains(name)) namesToDisable.Remove(name);
        }
        else
        {
            if (!namesToDisable.Contains(name)) namesToDisable.Add(name);
        }

        Canvas dropDownList = GetComponentInChildren<Canvas>();

        if (!dropDownList) return;

        Toggle[] toogles = dropDownList.GetComponentsInChildren<Toggle>(true);
        toogles.First(x => x.name == name).interactable = enable;
    }
}
