using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Transform>().GetComponentInParent<ObjectAudio>() != null)
        {
            if (other.GetComponent<Transform>().GetComponentInParent<ObjectAudio>().IsNotPlaying())
            {
                other.GetComponent<Transform>().GetComponentInParent<ObjectAudio>().PlayAudio();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Transform>().GetComponentInParent<ObjectAudio>() != null)
        {
            other.GetComponent<Transform>().GetComponentInParent<ObjectAudio>().StopAudio();
        }
    }
}
