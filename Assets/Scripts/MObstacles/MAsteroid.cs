using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAsteroid : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;

    public int speed = 1;

    public Vector3 direction;
    public float minDistanceToEarth;
    public float minDistanceToPlayer;

    private Vector3 playerPos;

    private bool passNearPlayer = false;

    void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.magenta;
    }

    void Update()
    {
        this.Move();
    }


    public void Move()
    {
        // en caso de que este lejos del jugador
        if(!passNearPlayer && Vector3.Distance(this.transform.position, this.player.transform.position) > 20f)
        {
            playerPos = this.player.transform.position;
            direction = (playerPos - this.transform.position).normalized;
        }
        // en caso de que este cerca del jugador por primera vez
        else if(!passNearPlayer && Vector3.Distance(this.transform.position, this.player.transform.position) <= 10f)
        {
            passNearPlayer = true;
        }
        // en caso de que se aleje del jugador
        else if(passNearPlayer && Vector3.Distance(this.transform.position, this.player.transform.position) > 10f)
        {
            Destroy(this.gameObject);
        }
        this.transform.position += direction * speed * Time.deltaTime;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.player.transform.position.z);
    }

    public void DamagePlayer(int force)
    {
        player.GetComponent<PlayerController>().hitPlayer(force);
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        this.minDistanceToEarth = 0.5f;
        this.minDistanceToPlayer = 1f;
        
        // se calcula la posicion en la que aparecera el obstaculo
        float zPosition = player.transform.position.z + 70f;

        // se calcula el frustum
        float distanceFromCameraZ = Vector3.Distance(new Vector3(0, 0, this.transform.position.z), Camera.main.transform.position);
        float frustumHeight = 2.0f * distanceFromCameraZ * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // se calcula la posicion en el eje x e y
        float xPosition = Random.Range(-frustumHeight * 1.1f, frustumHeight * 2f) * Mathf.Sign(Random.Range(-1f, 1f));
        float yPosition = Random.Range(-frustumHeight * 1.1f, frustumHeight * 2f) * Mathf.Sign(Random.Range(-1f, 1f));

        // se comprueba que la posicion este dentro del frustum
        while (Mathf.Abs(xPosition) < frustumHeight * 1.1f && Mathf.Abs(yPosition) < frustumHeight * 1.1f)
        {
            // se recalcula la posicion
            xPosition = Random.Range(-frustumHeight * 1.1f, frustumHeight * 2f) * Mathf.Sign(Random.Range(-1f, 1f));
            yPosition = Random.Range(-frustumHeight * 1.1f, frustumHeight * 2f) * Mathf.Sign(Random.Range(-1f, 1f));
        }

        Vector3 pos = new Vector3(xPosition, yPosition, zPosition);
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
