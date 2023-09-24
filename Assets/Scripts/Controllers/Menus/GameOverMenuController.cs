using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    void Start()
    {
        AddListeners();
    }
    private void AddListeners()
    {
        transform.Find("BtnMainMenu").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainMenu);
        transform.Find("BtnPlayAgain").gameObject.GetComponent<Button>().onClick.AddListener(PlayAgain);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menus");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
