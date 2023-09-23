using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    private AudioSource musicAudioSource;

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
        
    }

        private void UpdateSFXVolume(float volume)
    {

    }

    private void UpdateMasterVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
}