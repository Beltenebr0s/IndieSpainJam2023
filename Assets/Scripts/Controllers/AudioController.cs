using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField, BankRef] private string mainBank;

    [SerializeField] private Bus musicBus;
    [SerializeField] private Bus SFXBus;

    private EventInstance gameplayMusicI;
    private EventInstance menuMusicI;
    private EventInstance deathI;

    [SerializeField] private EventReference gameplayMusic;
    [SerializeField] private EventReference menuMusic;
    [SerializeField] private EventReference moonDeath;
    [SerializeField] private EventReference earthDeath;

    private void Awake()
    {
        RuntimeManager.LoadBank(mainBank, true);
        RuntimeManager.WaitForAllSampleLoading();
    }
    void Start()
    {
        musicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");

        menuMusicI = RuntimeManager.CreateInstance(menuMusic);
        gameplayMusicI = RuntimeManager.CreateInstance(gameplayMusic);


        menuMusicI.start();   
    }

    void Update()
    {
        foreach (var field in typeof(AudioParameters).GetFields())
        {
            gameplayMusicI.setParameterByName(field.Name, (float)field.GetValue(null));
        }
    }

    public void PlayGameplayMusic()
    {
        menuMusicI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameplayMusicI.start();
    }

    public void PlayMenuMusic() 
    {
        gameplayMusicI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        menuMusicI.start();
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

    public void SetVolumeMusic(float vol)
    {
        musicBus.setVolume(vol);
    }

    public void SetVolumeSFX(float vol)
    {
        SFXBus.setVolume(vol);
    }
}
