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
        this.transform.position += direction * speed * Time.deltaTime;
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
        
        Vector3 earthToPlayerVector = this.earth.transform.position - this.player.transform.position;
        Vector3 startingPoint = earthToPlayerVector.normalized *  earthToPlayerVector.magnitude / 2;

        direction = Random.value <= 0.5 ? Vector3.left : Vector3.right;

        float cameraAngle = Mathf.PI * 2 * (90 - Camera.main.fieldOfView / 2);
        float positionX = Mathf.Cos(cameraAngle) * earthToPlayerVector.magnitude / (2*Mathf.Sin(cameraAngle));
        
        Vector3 pos = startingPoint - direction * positionX;
        Debug.Log("Starting point: " + pos);
        Debug.Log("X: " + positionX);
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
