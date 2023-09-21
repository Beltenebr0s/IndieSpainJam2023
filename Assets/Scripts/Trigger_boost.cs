using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_boost : MonoBehaviour
{
    public float boostAmount = 5.0f; // Ajusta la velocidad
    public float boostDuration = 5.0f; // Duración del aumento de velocidad
    public GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (this.transform.position.z <= player.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("objeto detectado");
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 originalVelocity = rb.velocity;
                Debug.Log("Velocidad del jugador antes del aumento: " + rb.velocity);
                rb.velocity *= boostAmount;
                Debug.Log("Velocidad del jugador durante el aumento: " + rb.velocity);
                StartCoroutine(RestoreVelocity(rb, originalVelocity));
            }
        }
    }

    private IEnumerator RestoreVelocity(Rigidbody rb, Vector3 originalVelocity)
    {
        yield return new WaitForSeconds(boostDuration);
        rb.velocity = originalVelocity;
        Debug.Log("Velocidad del jugador después del boost: " + rb.velocity);
    }
}