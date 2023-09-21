using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference gameplayEvent;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(gameplayEvent);
        instance.start();   
    }

    void Update()
    {
        foreach (var field in typeof(AudioParameters).GetFields())
        {
            instance.setParameterByName(field.Name, (float)field.GetValue(null));
        }
    }
}
