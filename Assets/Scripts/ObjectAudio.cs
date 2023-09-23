using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class ObjectAudio : MonoBehaviour
{
    private EventInstance _instance;

    [SerializeField] private EventReference sustainAudio;
    [SerializeField] private EventReference impactAudio;

    void Start()
    {
        _instance = RuntimeManager.CreateInstance(sustainAudio);
        _instance.start();
        RuntimeManager.AttachInstanceToGameObject(_instance, gameObject.GetComponent<Transform>(), true);
    }

    public void PlayImpactAudio()
    {
        RuntimeManager.PlayOneShot(impactAudio);
    }

    public void StopAudio()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void OnDestroy()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
