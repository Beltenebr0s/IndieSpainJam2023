using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;

    public float speed = 1;

    public Vector3 direction;
    public float minDistanceToEarth;
    public float minDistanceToPlayer;

    private Vector3 playerPos;
    private Quaternion rotation;

    private bool passNearPlayer = false;

    private ObjectAudio objectAudio;

    public int damageValue = 1;
    
    void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.magenta;
        player = GameObject.Find("Player");

        objectAudio = GetComponent<ObjectAudio>();
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
        // rotar el asteroide para que mire en la direccion en la que se mueve
        rotation = Quaternion.LookRotation(direction);
        if(this.transform.rotation != rotation)
            this.transform.rotation = Quaternion.LookRotation(direction);

        // mover el asteroide
        this.transform.position += direction * speed * Time.deltaTime;

        // mantener el asteroide a la misma posicion z del jugador para asegurarse que le golpea
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.player.transform.position.z);
    }

    public void DamagePlayer(int force)
    {
        player.GetComponent<PlayerController>().hitPlayer(force);
        objectAudio.PlayImpactAudio();
    }

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        this.minDistanceToEarth = 0.5f;
        this.minDistanceToPlayer = 1f;
        
        // se calcula la posicion en la que aparecera el obstaculo
        float zPosition = player.transform.position.z;

        // se calcula el frustum
        float distanceFromCameraZ = Vector3.Distance(new Vector3(0, 0, zPosition), Camera.main.transform.position);
        float frustumHeight = 2.0f * distanceFromCameraZ * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumHeight *= 1.3f;   

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
    


    public void Collision(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            DamagePlayer(damageValue);
        }
        else if (collider.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collider.gameObject);
        }
        Destroy(this.gameObject);
    }

    public void setDamageValue(int damageValue)
    {
        this.damageValue = damageValue;
    }
}
