using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    GameObject player;
    List<GameObject> activeObstacles;

    List<GameObject> satellites;

    [Header("Spawn Settings")]
    public float timerToSpawn = 5f;
    public float tSpawn = 0f;
    public Transform spawnPosition;
    public float timerToDespawn = 3;
    public int satelliteAmount = 100;

    [Header("Idle Obstacles")]
    public List<GameObject> prefabAsteroid;
    public GameObject prefabGarbage;
    public GameObject prefabSatellite;

    [Header("Moving Obstacles")]
    public GameObject prefabMisile;
    public GameObject prefabBoomerang;
    
    void Start()
    {
        activeObstacles = new List<GameObject>();
        satellites = new List<GameObject>();
        
        player = GameObject.Find("Player");
        tSpawn = 0f;

        for (int i = 0; i < satelliteAmount; i++)
        {
            CreateSatellite();
        }
        
    }

    void Update()
    {
        if (!player.GetComponent<PlayerController>().enCaida)
        {
            return;
        }
        if (tSpawn >= timerToSpawn)
        {
            CreateIdleObstacle();
            CreateMovingObstacle();

            tSpawn = 0f;
        }
        tSpawn += Time.deltaTime;
    }
    
    public void CreateMovingObstacle()
    {
        float randomNumber = Random.value;
        GameObject newObstacle = null;
        if (randomNumber <= 0.5f)
        {
            newObstacle = GameObject.Instantiate(prefabMisile, spawnPosition.position, Quaternion.identity);
        }
        else
        {
            newObstacle = GameObject.Instantiate(prefabBoomerang, spawnPosition.position, Quaternion.identity);
        }

        newObstacle.transform.position = newObstacle.GetComponent<IMovingObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IMovingObstacle>().Throw();
        activeObstacles.Add(newObstacle);
        newObstacle.transform.parent = this.transform;
        StartCoroutine(DestroyObstacle(newObstacle));
    }

    public void CreateIdleObstacle()
    {
        float randomNumber = Random.value;
        GameObject newObstacle = null;
        if (randomNumber <= 0.5f)
        {
            newObstacle = GameObject.Instantiate(prefabAsteroid[Random.Range(0,prefabAsteroid.Count)], spawnPosition.position, Quaternion.identity);
        }
        else
        {
            newObstacle = GameObject.Instantiate(prefabGarbage, spawnPosition.position, Quaternion.identity);
        }

        newObstacle.transform.position = newObstacle.GetComponent<IIdleObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IIdleObstacle>().Move();
        activeObstacles.Add(newObstacle);
        newObstacle.transform.SetParent(this.transform);
    }

    private void CreateSatellite()
    {
        GameObject newObstacle = GameObject.Instantiate(prefabSatellite, spawnPosition.position, Quaternion.identity);
        newObstacle.transform.position = newObstacle.GetComponent<IIdleObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IIdleObstacle>().Move();
        satellites.Add(newObstacle);
        newObstacle.transform.SetParent(this.transform);
        newObstacle.GetComponent<Satellite>().FirstMove();
    }
    private void CreateAsteroid()
    {
        GameObject newObstacle = GameObject.Instantiate(prefabAsteroid[Random.Range(0,prefabAsteroid.Count)], spawnPosition.position, Quaternion.identity);
        newObstacle.transform.position = newObstacle.GetComponent<IIdleObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IIdleObstacle>().Move();
        satellites.Add(newObstacle);
        newObstacle.transform.SetParent(this.transform);
    }

    IEnumerator DestroyObstacle(GameObject obstacle)
    {
        yield return new WaitForSeconds(timerToDespawn);
        activeObstacles.Remove(obstacle);
        Destroy(obstacle);
    }

}
