using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour, IMovingObstacle
{
    public GameObject earth;
    public GameObject player;

    public int damageValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(){}

    public void DamagePlayer(int damageValue)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damageValue);
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        return Vector3.zero;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamagePlayer(damageValue);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Audio
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
    public void setDamageValue(int damageValue)
    {
        this.damageValue = damageValue;
    }
}
