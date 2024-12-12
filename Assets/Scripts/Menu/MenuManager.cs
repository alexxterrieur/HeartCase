using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject pauseMenu;
    public GameObject pausedsettingsMenu;
    public GameObject warningPopUp;
    
    
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        SceneManager.LoadScene("MiseEnCommun2");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void CloseAllMenus()
    {
        if (mainMenu != null) mainMenu.SetActive(false);
        if (settingsMenu != null) settingsMenu.SetActive(false);
        if (creditsMenu != null) creditsMenu.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (pausedsettingsMenu != null) pausedsettingsMenu.SetActive(false);
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
    
    public void CreditsMenu()
    {
        CloseAllMenus();
        creditsMenu.SetActive(true);
    }

    public void PauseMenu()
    {
        CloseAllMenus();
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        CloseAllMenus();
    }

    public void PausedSettingsMenu()
    {
        CloseAllMenus();
        pausedsettingsMenu.SetActive(true);
    }

    public void DisplayWarning(bool _active)
    {
        warningPopUp.SetActive(_active);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
