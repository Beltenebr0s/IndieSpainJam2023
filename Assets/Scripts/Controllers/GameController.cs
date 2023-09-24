using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Por si acaso la escena muere cuando haga commit, meted esto por el editor <3
    [Header("UI")]
    public List<RawImage> triesList; 
    public Texture tryIconEnabled;
    public Texture tryIconDisabled;
    public GameObject hud;
    public GameObject gameOverMenu;
    public TMP_Text scoreTextUI;
    public float score;

    private void Start()
    {
        RestartGame();
        foreach(RawImage img in triesList)
        {
            img.texture = tryIconEnabled;
        }
        hud.SetActive(true);
        gameOverMenu.SetActive(false);
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
            AudioParameters.MuteSFX = true;
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

    public void AddScore(float x)
    {
        score += x;
    }
    public void EndTurn()
    {
        startGame = false;
        Debug.Log("Ended turn: " + currentTry);
        triesList[currentTry].texture = tryIconDisabled;
        if (currentTry >= numTries-1)
        {
            EndGame();
        }
        else
        {
            RestartGame();
        }
        currentTry++;

        AudioParameters.Victory = 1;
    }

    public void RestartGame()
    {
        AudioParameters.Victory = 0;

        //Debug.Log("Start Game");
        player.transform.position = initialPlayerPosition.position;
        player.GetComponent<PlayerController>().Reset();
        earth.transform.position = initialEarthPosition.position;
        startGame = true;
        earth.GetComponent<PlanetController>().StartGame();
    }

    public void EndGame()
    {
        //Debug.Log("End Game");
        startGame = false;
        endGame = true;
        player.GetComponent<PlayerController>().EndGame();
        // Gameover Screen
        hud.SetActive(false);
        scoreTextUI.SetText("Final score: " + score);
        gameOverMenu.SetActive(true);
    }
}
