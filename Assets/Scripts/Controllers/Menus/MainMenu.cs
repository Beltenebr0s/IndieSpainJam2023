using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject Menus;
    private GameObject mainMenu;
    private GameObject settingsMenu;
    private GameObject controlMenu;
    private GameObject creditsMenu;

    void Start()
    {
        GetComponents();
        AddListeners();

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        controlMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    private void GetComponents()
    {
        mainMenu = Menus.transform.Find("MMainMenu").gameObject;
        settingsMenu = Menus.transform.Find("MSettings").gameObject;
        controlMenu = Menus.transform.Find("MControls").gameObject;
        creditsMenu = Menus.transform.Find("MCredits").gameObject;
    }

    private void AddListeners()
    {
        mainMenu.transform.Find("BtnPlay").gameObject.GetComponent<Button>().onClick.AddListener(PlayGame);
        mainMenu.transform.Find("BtnSettings").gameObject.GetComponent<Button>().onClick.AddListener(ShowSettingsMenu);
        mainMenu.transform.Find("BtnControls").gameObject.GetComponent<Button>().onClick.AddListener(ShowControlsMenu);
        mainMenu.transform.Find("BtnCredits").gameObject.GetComponent<Button>().onClick.AddListener(ShowCreditsMenu);
        mainMenu.transform.Find("BtnExit").gameObject.GetComponent<Button>().onClick.AddListener(ExitGame);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void ShowControlsMenu()
    {
        controlMenu.SetActive(true);
    }

    public void ShowCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}