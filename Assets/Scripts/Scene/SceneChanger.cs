using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown sceneDropDown;
    private int sceneId;

    public void ChangeSceneId() 
    {
        sceneId = sceneDropDown.value;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneId);
    }
}
