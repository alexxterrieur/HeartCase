using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown sceneDropDown;
    private string sceneName;

    public void ChangeSceneName() 
    {
        sceneName = sceneDropDown.name;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
