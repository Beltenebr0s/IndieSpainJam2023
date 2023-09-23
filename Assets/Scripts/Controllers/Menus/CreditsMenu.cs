using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuController : MonoBehaviour
{
    void Start()
    {
        AddListeners();
    }
    private void AddListeners()
    {
        transform.Find("BtnBack").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainMenu);
    }
    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
    }
}