using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum EDificultad
{
    Facil,
    Normal,
    Dificil,
    Libre
}

public class ObstacleSpawner : MonoBehaviour
{
    private  GameObject player;
    private List<GameObject> activeObstacles;

    public EDificultad dificultad = EDificultad.Facil;

    List<GameObject> satellites;

    [Header("Spawn Settings")]
    private float tSpawn = 0f;
    //private Transform spawnPosition;
    public float timerToSpawn = 5f;
    public int satelliteAmount = 100;
    public int demageValue = 1; 

    [Header("Idle Obstacles")]
    public List<GameObject> prefabAsteroid;
    public GameObject prefabGarbage;
    public GameObject prefabSatellite;

    [Header("Moving Obstacles")]
    public GameObject prefabMisile;
    public GameObject prefabBoomerang;

    public bool enCaida = false;
    

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        enCaida = false;
        DestroyObstacles();
        switch (dificultad)
        {
            case EDificultad.Facil:
                timerToSpawn = 3f;
                satelliteAmount = 500;
                demageValue = 1;
                break;
            case EDificultad.Normal:
                timerToSpawn = 1f;
                satelliteAmount = 750;
                demageValue = 2;
                break;
            case EDificultad.Dificil:
                timerToSpawn = 0.5f;
                satelliteAmount = 1000;
                demageValue = 3;
                break;
        }

        activeObstacles = new List<GameObject>();
        satellites = new List<GameObject>();
        
        player = GameObject.Find("Player");
        tSpawn = 0f;

        for (int i = 0; i < satelliteAmount; i++)
        {
            CreateSatellite();
        }
    }

    public void EndGame()
    {
        DestroyObstacles();
        enCaida = false;
    }

    public void DestroyObstacles()
    {
        foreach (Transform child in transform)
        {
            if(child.name != "StartingPosition")
                Destroy(child.gameObject);
        }
        foreach (GameObject warm in GameObject.FindGameObjectsWithTag("WarmUI"))
        {
            Destroy(warm);
        }
    }

    void Update()
    {
        if (!enCaida && player.GetComponent<PlayerController>().enCaida)
        {
            enCaida = true;
        }
        if(enCaida){
            if (tSpawn >= timerToSpawn)
            {
                CreateIdleObstacle();
                CreateMovingObstacle();

                tSpawn = 0f;
            }
            tSpawn += Time.deltaTime;
        }    
    }
    public void CambiarDificultad(EDificultad eDificultad)
    {
        dificultad = eDificultad;
    }
    
    public void CreateMovingObstacle()
    {
        float randomNumber = Random.value;
        GameObject newObstacle = null;
        if (randomNumber <= 0.5f)
        {
            newObstacle = GameObject.Instantiate(prefabMisile);
        }
        else
        {
            newObstacle = GameObject.Instantiate(prefabBoomerang);
        }

        newObstacle.transform.position = newObstacle.GetComponent<IMovingObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IMovingObstacle>().Throw();
        newObstacle.GetComponent<IMovingObstacle>().setDamageValue(demageValue);
        activeObstacles.Add(newObstacle);
        newObstacle.transform.parent = this.transform;
    }

    public void CreateIdleObstacle()
    {
        float randomNumber = Random.value;
        GameObject newObstacle = null;
        if (randomNumber <= 0.5f)
        {
            newObstacle = GameObject.Instantiate(prefabAsteroid[Random.Range(0,prefabAsteroid.Count)]);
        }
        else
        {
            newObstacle = GameObject.Instantiate(prefabGarbage);
        }

        newObstacle.transform.position = newObstacle.GetComponent<IIdleObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IIdleObstacle>().Move();
        newObstacle.GetComponent<IIdleObstacle>().setDamageValue(demageValue);
        activeObstacles.Add(newObstacle);
        newObstacle.transform.SetParent(this.transform);
    }

    private void CreateSatellite()
    {
        GameObject newObstacle = GameObject.Instantiate(prefabSatellite);
        newObstacle.transform.position = newObstacle.GetComponent<IIdleObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IIdleObstacle>().Move();
        newObstacle.GetComponent<IIdleObstacle>().setDamageValue(demageValue);
        satellites.Add(newObstacle);
        newObstacle.transform.SetParent(this.transform);
        newObstacle.GetComponent<Satellite>().FirstMove();
    }
}