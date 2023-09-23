using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGarbage : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;
    public GameObject gameController;

    private float minDistanceToEarth;

    public float forceMax = 1f;
    public float change = 0.01f;
    private int increm = -1;
    public float force;

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

        if (Mathf.Abs(force) > forceMax)
        {
            increm *= -1; 
        }
        force += change * increm;
    }

    public void DamagePlayer(int damageValue)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damageValue);
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController");
        float frustumWidth = gameController.GetComponent<GameController>().frustumWidth;

        force = forceMax;

        float x = Random.Range(-frustumWidth, frustumWidth);
        float y = Random.Range(-frustumWidth, frustumWidth);
        float z = Random.Range(player.transform.position.z + 20f, player.transform.position.z + 30f);

        Vector3 pos = new Vector3(x, y, z);
        
        return pos;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer(1);
            
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Audio
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}
