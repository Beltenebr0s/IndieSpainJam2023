using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    void Start()
    {
        AddListeners();
    }
    private void AddListeners()
    {
        transform.Find("BtnBack").gameObject.GetComponent<Button>().onClick.AddListener(BackToMainMenu);
        transform.Find("SliderMusic").gameObject.GetComponent<Slider>().onValueChanged.AddListener(UpdateMusicVolume);
        transform.Find("SliderSFX").gameObject.GetComponent<Slider>().onValueChanged.AddListener(UpdateSFXVolume);
        transform.Find("SliderMaster").gameObject.GetComponent<Slider>().onValueChanged.AddListener(UpdateMasterVolume);
    }
    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
    }

    private void UpdateMusicVolume(float volume)
    {
        AudioParameters.MusicVol = volume;
    }

        private void UpdateSFXVolume(float volume)
    {
        AudioParameters.SFXVol = volume;
    }

    private void UpdateMasterVolume(float volume)
    {
        AudioParameters.MasterVol = volume;
    }
}