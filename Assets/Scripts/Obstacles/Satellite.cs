using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour, IIdleObstacle
{
    private GameObject gameController;
    private GameObject earth;
    private GameObject player;
    private Vector3 direction;
    public float minSpeed = 30f;
    public float maxSpeed = 70f;
    private float speed;
    public float minSize = 0.1f;
    public float maxSize = 0.7f;
    private bool near = false;
    private ObjectAudio objectAudio;
    public int damageValue = 1;
    private Vector3 initialPlayerPosition;
    private Vector3 initialEarthPosition;

    void Start()
    {
        objectAudio = GetComponent<ObjectAudio>();
    }

    void Update()
    {
        this.Move();

        // destruir el satelite si se queda muy atras
        if(this.transform.position.z < player.transform.position.z - 50f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        // mover el satelite
        this.transform.LookAt(earth.transform);
        this.transform.RotateAround(earth.transform.position, direction, speed * Time.deltaTime);
    }

    public void FirstMove()
    {
        // asignarles una posicion inicial aleatoria dentro de la rotacion calculada previamente
        this.transform.LookAt(earth.transform);
        this.transform.RotateAround(earth.transform.position, direction, Random.Range(speed * 10, speed * 20));
    }

    public void DamagePlayer(int force)
    {
        player.GetComponent<PlayerController>().hitPlayer(force);
        objectAudio.PlayImpactAudio();
    }

    public Vector3 FindStartingPosition()
    {
        gameController = GameObject.Find("GameController");
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        initialPlayerPosition = GameObject.Find("InitialPlayerPosition").transform.position;
        initialEarthPosition = GameObject.Find("InitialEarthPosition").transform.position;

        // calcular el frustum para limitar la posicion inicial de los satelites
        float frustumWidth = gameController.GetComponent<GameController>().frustumWidth;

        // colocarlos siguiendo una distribucion cuadratica
        float zP = initialPlayerPosition.z - initialEarthPosition.z;
        Vector3 pos = new Vector3(Random.Range(-frustumWidth, frustumWidth), Random.Range(-frustumWidth, frustumWidth), (Mathf.Sqrt(Random.Range(0f, 1f))) * zP);

        // calcular la direccion y la velocidad aleatoriamente
        this.direction = new Vector3(Random.value, Random.value, Random.value).normalized;
        speed = Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minSpeed, maxSpeed);

        // calcular el tamaño aleatoriamente
        this.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

        return pos;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(damageValue);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }

    public void setDamageValue(int damageValue)
    {
        this.damageValue = damageValue;
    }
}
