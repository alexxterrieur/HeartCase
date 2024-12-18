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
        if (SceneManager.GetActiveScene().name == "Bar" && !GameState.Instance.GetBool(3, 1) && GameState.Instance.GetBool(2, 6))
        {
            SceneManager.LoadScene("Outside");
            return;
        }

        AudioManager.Instance.PlaySFX("Scene-Change");

        SceneManager.LoadScene(sceneName);
    }

    public void AddOption(string option, bool displayPopUp = true, bool gameStateCheck = true)
    {
        sceneDropDown.GetComponent<DropDownController>().AddOption(option, displayPopUp, gameStateCheck);
    }
}
