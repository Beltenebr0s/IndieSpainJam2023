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
    public float firstHealthThreshold = 980;
    public float secondHealthThreshold = 960;

    public ObstacleSpawner objectSpawn;
    void Start()
    {
        health = maxHealth;
    }

    public void StartGame()
    {
        startGame = true;
        if (health <= secondHealthThreshold)
        {
            Debug.Log("Difícil");
            objectSpawn.CambiarDificultad(EDificultad.Dificil);
        }
        else if (health <= firstHealthThreshold)
        {
            Debug.Log("Medio");
            objectSpawn.CambiarDificultad(EDificultad.Normal);
        }
        else
        {
            Debug.Log("Fácil");
            objectSpawn.CambiarDificultad(EDificultad.Facil);
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
