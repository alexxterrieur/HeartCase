using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStateManager : MonoBehaviour
{
    [SerializeField] private Dropdown sceneDropDown;
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
