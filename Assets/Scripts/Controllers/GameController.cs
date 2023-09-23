using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool startGame = false;
    private bool endGame = false;

    [Header("LevelSettings")]
    public int numTries = 3;
    public int currentTry = 0;
    public Transform initialPlayerPosition;
    public Transform initialEarthPosition;

    [System.NonSerialized]
    public float frustumWidth;

    public GameObject earth;
    public GameObject player;
    public GameObject pauseMenu;

    [Header("Boosts Settings")]
    [SerializeField]
    private float minTimeBetweenBoosts = 5;
    [SerializeField]
    private float maxTimeBetweenBoosts = 10;
    private float timeSinceLastBoosts = 0f;
    private float timeToNextBoosts = 0f;
    [SerializeField]
    private List<GameObject> boostsList;

    private void Start()
    {
        RestartGame();
        timeToNextBoosts = Random.Range(minTimeBetweenBoosts, maxTimeBetweenBoosts);
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(-frustumWidth, -frustumWidth, player.transform.position.z), new Vector3(frustumWidth, -frustumWidth, player.transform.position.z), Color.red);
        Debug.DrawLine(new Vector3(-frustumWidth, frustumWidth, player.transform.position.z), new Vector3(frustumWidth, frustumWidth, player.transform.position.z), Color.red);
        Debug.DrawLine(new Vector3(-frustumWidth, -frustumWidth, player.transform.position.z), new Vector3(-frustumWidth, frustumWidth, player.transform.position.z), Color.red);
        Debug.DrawLine(new Vector3(frustumWidth, -frustumWidth, player.transform.position.z), new Vector3(frustumWidth, frustumWidth, player.transform.position.z), Color.red);

        if (startGame)
        {
            timeSinceLastBoosts += Time.deltaTime;
            if (timeSinceLastBoosts >= timeToNextBoosts)
            {
                addBoost();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pause Game");
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void addBoost()
    {
        timeSinceLastBoosts = 0;
        timeToNextBoosts = Random.Range(minTimeBetweenBoosts, maxTimeBetweenBoosts);
        GameObject boost = Instantiate(boostsList[Random.Range(0, boostsList.Count)]);
        boost.transform.parent = this.transform;
        locateBoost(boost);
    }

    private void locateBoost(GameObject boost)
    {

        float x = Random.Range(-frustumWidth, frustumWidth);
        float y = Random.Range(-frustumWidth, frustumWidth);
        float z = Random.Range(player.transform.position.z + 50, player.transform.position.z + 100);
        boost.transform.position = new Vector3(x, y, z);
    }

    public void EndTurn()
    {
        startGame = false;
        Debug.Log("Ended turn: " + currentTry);
        if (currentTry >= numTries)
        {
            EndGame();
        }
        else
        {
            RestartGame();
        }
        currentTry++;
    }

    public void RestartGame()
    {
        Debug.Log("Start Game");
        player.transform.position = initialPlayerPosition.position;
        player.GetComponent<PlayerController>().Reset();
        earth.transform.position = initialEarthPosition.position;
        startGame = true;
        earth.GetComponent<PlanetController>().StartGame();
    }

    public void EndGame()
    {
        Debug.Log("End Game");
        startGame = false;
        endGame = true;
        player.GetComponent<PlayerController>().EndGame();
        // Gameover Screen
    }
}
