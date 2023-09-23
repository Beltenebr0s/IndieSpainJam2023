using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private FMOD.Studio.EventInstance gameplayMusicI;
    private FMOD.Studio.EventInstance menuMusicI;
    private FMOD.Studio.EventInstance deathI;
    [SerializeField] private FMODUnity.EventReference gameplayMusic;
    [SerializeField] private FMODUnity.EventReference menuMusic;
    [SerializeField] private FMODUnity.EventReference moonDeath;
    [SerializeField] private FMODUnity.EventReference earthDeath;

    void Start()
    {
        menuMusicI = FMODUnity.RuntimeManager.CreateInstance(menuMusic);
        gameplayMusicI = FMODUnity.RuntimeManager.CreateInstance(gameplayMusic);
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
        deathI = FMODUnity.RuntimeManager.CreateInstance(moonDeath);
        deathI.start();
    }

    public void PlayVictory()
    {
        deathI = FMODUnity.RuntimeManager.CreateInstance(earthDeath);
        deathI.start();
    }
}
