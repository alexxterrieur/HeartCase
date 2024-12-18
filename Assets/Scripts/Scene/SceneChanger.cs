using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown sceneDropDown;
    private string sceneName;

    public void ChangeSceneName()
    {
        sceneName = sceneDropDown.options[sceneDropDown.value].text;
    }

    public void ChangeScene()
    {
        if (!GameState.Instance.GetBool(3, 1) && GameState.Instance.GetBool(2, 6) && SceneManager.GetActiveScene().name == "Bar")
        {
            SceneManager.LoadScene("Outside");
        }

        SceneManager.LoadScene(sceneName);
    }

    public void AddOption(string option, bool displayPopUp = true, bool gameStateCheck = true)
    {
        sceneDropDown.GetComponent<DropDownController>().AddOption(option, displayPopUp, gameStateCheck);
    }
}
