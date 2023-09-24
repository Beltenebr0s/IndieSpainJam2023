using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private bool startGame = false;
    private bool endGame = false;

    public GameController gameController;

    // Ajustar estas movidas
    [Header("Health Settings")]
    public float maxHealth = 1000;
    public float health;
    public float firstHealthThreshold = 950;
    public float secondHealthThreshold = 900;

    [Header("Spawners")]
    public GameObject spawnEasy;
    public GameObject spawnMedium;
    public GameObject spawnHard;
    public GameObject activeSpawn;
    void Start()
    {
        activeSpawn = spawnEasy;
        health = maxHealth;
    }

    public void StartGame()
    {
        startGame = true;
        // Cambiar aspecto de la tierra según la vida que le quede
        if (health <= secondHealthThreshold)
        {
            activeSpawn = spawnHard;
        }
        else if (health <= firstHealthThreshold)
        {
            activeSpawn = spawnMedium;
        }
        else
        {
            activeSpawn = spawnEasy;
        }
    }

    public void EndGame()
    {
        startGame = false;
        endGame = true;
        gameController.EndTurn();
    }

    public void HandleCollision(bool earthWasHit, GameObject player)
    {
        if (earthWasHit)
        {
            float hitSpeed = player.GetComponent<Rigidbody>().velocity.magnitude;
            Debug.Log("Daño: " + hitSpeed);
            health -= hitSpeed;
            gameController.AddScore(hitSpeed);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        EndGame();
    }

}
