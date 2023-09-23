using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthNonCollider : MonoBehaviour
{
    public GameObject earth;
    public GameObject player;
    void Start()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            earth.GetComponent<PlanetController>().HandleCollision(false, player);
        }
    }
}
