using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private bool startGame = false;
    private bool endGame = false;

    public GameController gameController;


    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        startGame = true;
    }

    public void EndGame()
    {
        startGame = false;
        endGame = true;
        gameController.EndTurn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Llegó a la tierra");
            EndGame();
        }
    }
}
