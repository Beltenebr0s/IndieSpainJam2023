using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCollision : MonoBehaviour
{
    public GameObject earth;
    public GameObject player;

    [SerializeField] private EventReference crash;
    void Start()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            earth.GetComponent<PlanetController>().HandleCollision(true, player);
            RuntimeManager.PlayOneShot(crash);
        }
    }
}
