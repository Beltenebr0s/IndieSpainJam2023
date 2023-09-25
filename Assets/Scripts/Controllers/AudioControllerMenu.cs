using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerMenu : MonoBehaviour
{
    [SerializeField, BankRef] private string bank;

    [SerializeField] private Bus masterBus;
    [SerializeField] private Bus musicBus;
    [SerializeField] private Bus SFXBus;

    private EventInstance menuMusicI;

    [SerializeField] private EventReference menuMusic;

    private void Awake()
    {
        RuntimeManager.LoadBank(bank, true);
        RuntimeManager.WaitForAllSampleLoading();
    }

    void Start()
    {
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");

        menuMusicI = RuntimeManager.CreateInstance(menuMusic);

        menuMusicI.start();
    }

    void Update()
    {
        if (AudioParameters.MuteSFX)
        {
            SFXBus.setVolume(0);
        }
        else
        {
            SFXBus.setVolume(AudioParameters.SFXVol);
        }

        if (AudioParameters.MuteMusic)
        {
            musicBus.setVolume(0);
        }
        else
        {
            musicBus.setVolume(AudioParameters.MusicVol);
        }

        if (AudioParameters.Mute)
        {
            masterBus.setVolume(0);
        }
        else
        {
            masterBus.setVolume(AudioParameters.MasterVol);
        }
        
    }

    private void OnDisable()
    {
        menuMusicI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
