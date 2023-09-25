using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour, IIdleObstacle
{
    private GameObject earth;
    private GameObject player;
    private GameObject gameController;
    private float minDistanceToEarth;

    public float forceMax = 1f;
    public float change = 0.01f;
    private int increm = -1;
    private float force;
    private float rotationDirection;
    public int damageValue = 1;
    private Vector3 initialPlayerPosition;
    private Vector3 initialEarthPosition;

    private void Start() {
        rotationDirection = Mathf.Sign(Random.Range(-1f, 1f));
    }

    void Update()
    {
        this.Move();

        if (this.transform.position.z < player.transform.position.z - 10)
        {
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        this.transform.Translate(new Vector3(0, 1, 0) * force * Time.deltaTime);
        transform.Rotate(new Vector3(0, 1, 0) * 50 * Time.deltaTime * rotationDirection);

        if (Mathf.Abs(force) > forceMax)
        {
            increm *= -1; 
        }
        force += change * increm;
    }

    public void DamagePlayer(int damageValue)
    {
        player.GetComponent<PlayerController>().hitPlayer(damageValue);
    }

    public Vector3 FindStartingPosition()
    {
        gameController = GameObject.Find("GameController");
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        initialPlayerPosition = GameObject.Find("InitialPlayerPosition").transform.position;
        initialEarthPosition = GameObject.Find("InitialEarthPosition").transform.position;
        float frustumWidth = gameController.GetComponent<GameController>().frustumWidth;

        force = forceMax;

        float x = Random.Range(-frustumWidth, frustumWidth);
        float y = Random.Range(-frustumWidth, frustumWidth);
        float z = Random.Range(initialPlayerPosition.z + 20f, initialEarthPosition.z - 30f);
        


        Vector3 pos = new Vector3(x, y, z);

        this.transform.Rotate(new Vector3(0, 0, Random.Range(-20f, 20f)));

        return pos;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(damageValue);
            
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            // Audio
            Destroy(other.gameObject);
        }
    }

    public void setDamageValue(int damageValue)
    {
        this.damageValue = damageValue;
    }
}
