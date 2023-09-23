using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        AddListeners();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
        {
            Resume();
        }
    }
    private void AddListeners()
    {
        transform.Find("BtnResume").gameObject.GetComponent<Button>().onClick.AddListener(Resume);
        transform.Find("BtnRestart").gameObject.GetComponent<Button>().onClick.AddListener(Restart);
        transform.Find("BtnExit").gameObject.GetComponent<Button>().onClick.AddListener(Exit);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menus");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
}
