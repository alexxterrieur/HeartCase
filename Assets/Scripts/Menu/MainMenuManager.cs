using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    public void Play()
    {
        SceneManager.LoadScene("MiseEnCommun");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void MainMenu()
    {
        CloseAllMenus();
        mainMenu.SetActive(true);
    }

    public void SettingsMenu()
    {
        CloseAllMenus();
        settingsMenu.SetActive(true);
    }
}
