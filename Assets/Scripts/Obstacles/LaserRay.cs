using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour, IMovingObstacle
{
    public GameObject earth;
    public GameObject player;
    bool runAimTimer = false;
    public float timerToAim = 3f;
    public float tAim = 0f;
    bool runShootTimer = false; 
    public float timerToShoot = 0.6f;
    public float tShoot = 0f;
    Vector3 targetPosition = Vector3.zero;
    
    void Update()
    {
        Debug.DrawLine(this.transform.position, targetPosition);
        if(Input.GetKeyDown(KeyCode.F))
        {
            Throw();
        }
        // Timer para seguir al jugador y apuntar
        if (runAimTimer)
        {
            if (tAim >= timerToAim)
            {
                runAimTimer = false;
                runShootTimer = true;
                tAim = 0f;
            }
            targetPosition = player.transform.position;
            tAim += Time.deltaTime;
        }
        // Mini timer para que haya un tiempo entre que apunta
        // y dispara para que no sea absurdamente injusto
        if (runShootTimer)
        {
            if (tShoot >= timerToShoot)
            {
                runShootTimer = false;
                tShoot = 0f;
                ShootLaser();
                Destroy(this.gameObject);
            }
            targetPosition = player.transform.position;
            tShoot += Time.deltaTime;
        }
    }

    private void ShootLaser()
    {
        // TODO: meter el raycast para ver si le ha dado al jugador
    }

    public void Throw()
    {
        runAimTimer = true;
    }

    public void DamagePlayer(int damageValue)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damageValue);
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        return earth.transform.position + Vector3.forward * 4f;
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
