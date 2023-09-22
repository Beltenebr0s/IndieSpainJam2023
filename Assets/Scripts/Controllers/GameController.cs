using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool startGame = false;
    private bool endGame = false;

    [System.NonSerialized]
    public float frustumWidth;

    public GameObject earth;
    public GameObject player;

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

    public void StartGame()
    {
        Debug.Log("Start Game");
        startGame = true;
        earth.GetComponent<PlanetController>().StartGame();
    }

    public void EndGame()
    {
        Debug.Log("End Game");
        startGame = false;
        endGame = true;
        player.GetComponent<PlayerController>().EndGame();
    }
}
