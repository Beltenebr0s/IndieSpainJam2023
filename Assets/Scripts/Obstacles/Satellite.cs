using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;

    public Vector3 direction;
    private float minDistanceToEarth;
    
    void Update()
    {
        this.Move();
    }

    public void Move()
    {
        this.transform.LookAt(earth.transform);
        this.transform.RotateAround(earth.transform.position, direction, 50 * Time.deltaTime);
    }

    public void DamagePlayer(int damageValue)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damageValue);
        // Audio
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        this.minDistanceToEarth = 0.5f;

        float earthToPlayerDistance = (this.earth.transform.position - this.player.transform.position).magnitude;
        
        Vector3 pos = Random.insideUnitSphere * earthToPlayerDistance / 2 + 
                    new Vector3(this.minDistanceToEarth, this.minDistanceToEarth, this.minDistanceToEarth);
        
        this.direction = new Vector3(Random.value, Random.value, Random.value).normalized;
        
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
