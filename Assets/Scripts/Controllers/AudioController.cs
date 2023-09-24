using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField, BankRef] private string mainBank;

    [SerializeField] private Bus masterBus;
    [SerializeField] private Bus musicBus;
    [SerializeField] private Bus SFXBus;

    private EventInstance gameplayMusicI;
    private EventInstance deathI;

    [SerializeField] private EventReference gameplayMusic;
    [SerializeField] private EventReference moonDeath;
    [SerializeField] private EventReference earthDeath;

    private List<string> param;

    private void Awake()
    {
        RuntimeManager.LoadBank(mainBank, true);
        RuntimeManager.WaitForAllSampleLoading();
    }
    void Start()
    {
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");
        
        gameplayMusicI = RuntimeManager.CreateInstance(gameplayMusic);

        gameplayMusicI.start();

        AudioParameters.Death = 0;

        param = new List<string>{"Distance", "Death", "Victory"};
    }

    void Update()
    {
        foreach (var field in param)
        {
            gameplayMusicI.setParameterByName(field, (float)typeof(AudioParameters).GetField(field).GetValue(null));
        }

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
            musicBus.setVolume(AudioParameters.SFXVol);
        }

        if (AudioParameters.Mute)
        {
            masterBus.setVolume(0);
        }
        else
        {
            masterBus.setVolume(AudioParameters.SFXVol);
        }
    }

    public void PlayDeath()
    {
        deathI = RuntimeManager.CreateInstance(moonDeath);
        deathI.start();
    }

    public void PlayVictory()
    {
        deathI = RuntimeManager.CreateInstance(earthDeath);
        deathI.start();
    }

    private void OnDisable()
    {
        gameplayMusicI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
