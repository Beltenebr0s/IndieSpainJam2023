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
        Vector3 directionToPlayer = (this.transform.position - player.transform.position);
        // Forma vectorial de la ecuación de gravitación universal para dirigir un poco el misil
        // hacia la luna :')
        Vector3 fakeGravityForce = (- (rb.mass * player.GetComponent<Rigidbody>().mass) / directionToPlayer.magnitude)
                                 * directionToPlayer.normalized;
        rb.AddForce(fakeGravityForce, ForceMode.Force);
        Debug.DrawLine(this.transform.position, fakeGravityForce);
    }
    public void DamagePlayer(int damageValue){}

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
        return earth.transform.position + Vector3.forward * 4f;
    }
    
}
