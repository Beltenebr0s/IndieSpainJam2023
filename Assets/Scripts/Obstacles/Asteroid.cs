using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;

    public float speed = 5f;
    public Vector3 direction;
    public float minDistanceToEarth;
    public float minDistanceToPlayer;

    void Update()
    {
        this.Move();
    }

    public void Move()
    {
        this.transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void DamagePlayer(int damageValue)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damageValue);
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        this.minDistanceToEarth = 0.5f;
        this.minDistanceToPlayer = 1f;
        
        Vector3 earthToPlayerVector = this.player.transform.position - this.earth.transform.position;
        Debug.Log("Distancia a la tierra: " + earthToPlayerVector.magnitude);
        Vector3 startingPoint = earthToPlayerVector.normalized * 
                                Random.Range(minDistanceToEarth, earthToPlayerVector.magnitude / 2 - minDistanceToPlayer);

        direction = Random.value <= 0.5 ? Vector3.left : Vector3.right;

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.aspect * halfHeight;
        
        Vector3 pos = startingPoint + direction * halfWidth;
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
