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
        RuntimeManager.AttachInstanceToGameObject(_instance, gameObject.GetComponent<Transform>(), true);
    }

    public void PlayImpactAudio()
    {
        RuntimeManager.PlayOneShot(impactAudio);
    }

    public void PlayAudio()
    {
        _instance.start();
        RuntimeManager.AttachInstanceToGameObject(_instance, gameObject.GetComponent<Transform>(), true);
    }

    public void StopAudio()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnDestroy()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public bool IsNotPlaying()
    {
        PLAYBACK_STATE state;
        _instance.getPlaybackState(out state);
        return state == PLAYBACK_STATE.STOPPED; 
    }
}
