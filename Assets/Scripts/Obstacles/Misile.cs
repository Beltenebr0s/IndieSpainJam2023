using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile : MonoBehaviour, IMovingObstacle
{

    public GameObject earth;
    public GameObject player;
    public float force;
    public Vector3 direction;
    public Rigidbody rb;
    public Vector3 finalposition;


    void Update()
    {
        Move();
        //que gire que siempre mola mas
        transform.Rotate(Vector3.up * -10);
    }

    public void Throw()
    {
        Vector3 randomPointInSphere = Random.insideUnitSphere;
        // Asegurarse de que está entre la Tierra y la Luna para que no
        // se lance hacia atrás
        if (randomPointInSphere.z < earth.transform.position.z)
        {
            randomPointInSphere.z = -randomPointInSphere.z;
        }
        finalposition = player.transform.position;
        direction = finalposition - randomPointInSphere;
        rb.AddForce(force * direction.normalized, ForceMode.Impulse);
        transform.Rotate(Vector3.right * -90);
    }
    private void Move()
    {
        if ( player == null )
        {
            Debug.Log("Dónde está el jugador >:(");
            player = GameObject.Find("Player");
        }
        if ( rb == null)
        {
            Debug.Log("Ay perdí mi Rigidbody");
            rb = this.GetComponent<Rigidbody>();
        }
        Debug.DrawLine(this.transform.position, finalposition);
        float distanceToTarget = Vector3.Distance(this.transform.position, finalposition);
    if (distanceToTarget < 1.0f) 
    {
        Destroy(this.gameObject);
    }
    }
    public void DamagePlayer(int damageValue)
    {
        if ( player == null )
        {
            Debug.Log("Dónde está el jugador >:(");
            player = GameObject.Find("Player");
        }
        player.GetComponent<PlayerHealth>().TakeDamage(damageValue);
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
        return earth.transform.position - Vector3.forward * 4f;
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
