using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float timerToSpawn = 5f;
    public float tSpawn = 0f;
    public Transform spawnPosition;
    [Header("Idle Obstacles")]
    public GameObject prefabAsteroid;
    public GameObject prefabGarbage;
    public GameObject prefabSatellite;

    [Header("Moving Obstacles")]
    public GameObject prefabLaser;
    public GameObject prefabMisile;
    public GameObject prefabBoomerang;
    
    void Start()
    {
        tSpawn = 0f;
    }

    void Update()
    {
        if (tSpawn >= timerToSpawn)
        {
            if (Random.value <= 0.5f)
            {
                CreateIdleObstacle();
            }
            else
            {
                CreateMovingObstacle();
            }
            tSpawn = 0f;
        }
        tSpawn += Time.deltaTime;
    }

    public void CreateMovingObstacle()
    {
        float randomNumber = Random.value;
        GameObject newObstacle = null;
        if (randomNumber <= 0.33f)
        {
            newObstacle = GameObject.Instantiate(prefabLaser, spawnPosition.position, Quaternion.identity);
        }
        else if (randomNumber <= 0.66f)
        {
            newObstacle = GameObject.Instantiate(prefabMisile, spawnPosition.position, Quaternion.identity);
        }
        else
        {
            newObstacle = GameObject.Instantiate(prefabBoomerang, spawnPosition.position, Quaternion.identity);
        }

        newObstacle.transform.position = newObstacle.GetComponent<IMovingObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IMovingObstacle>().Throw();
    }

    public void CreateIdleObstacle()
    {
        float randomNumber = Random.value;
        GameObject newObstacle = null;
        if (randomNumber <= 0.33f)
        {
            newObstacle = GameObject.Instantiate(prefabAsteroid, spawnPosition.position, Quaternion.identity);
        }
        else if (randomNumber <= 0.66f)
        {
            newObstacle = GameObject.Instantiate(prefabGarbage, spawnPosition.position, Quaternion.identity);
        }
        else
        {
            newObstacle = GameObject.Instantiate(prefabSatellite, spawnPosition.position, Quaternion.identity);
        }
        newObstacle.transform.position = newObstacle.GetComponent<IIdleObstacle>().FindStartingPosition();
        newObstacle.GetComponent<IIdleObstacle>().Move();
    }

}
