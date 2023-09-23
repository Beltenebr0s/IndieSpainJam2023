using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private bool startGame = false;
    private bool endGame = false;

    public GameController gameController;

    public float maxHealth = 1000;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    public void StartGame()
    {
        startGame = true;
        // Cambiar aspecto de la tierra según la vida que le quede
        // if (health <= secondHealthThreshold)
        // {

        // }
        // else if (health <= firstHealthThresHold)
        // {

        // }
        // else
        // {

        // }
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
        }
        EndGame();
    }

}
