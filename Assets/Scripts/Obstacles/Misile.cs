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


    void Update()
    {
        Move();
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
        direction = randomPointInSphere - earth.transform.position;
        rb.AddForce(force * direction.normalized, ForceMode.Impulse);
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
        Vector3 directionToPlayer = (this.transform.position - player.transform.position);
        // Forma vectorial de la ecuación de gravitación universal para dirigir un poco el misil
        // hacia la luna :')
        Vector3 fakeGravityForce = (- (rb.mass * player.GetComponent<Rigidbody>().mass) / directionToPlayer.magnitude)
                                 * directionToPlayer.normalized;
        rb.AddForce(fakeGravityForce, ForceMode.Force);
        Debug.DrawLine(this.transform.position, fakeGravityForce);
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
        Debug.Log("Hola??");
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
