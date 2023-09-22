using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSatellite : MonoBehaviour, IIdleObstacle
{
    private GameObject gameController;
    public GameObject earth;
    public GameObject player;

    public Vector3 direction;

    public float minSpeed = 30f;
    public float maxSpeed = 70f;
    private float speed;
    private float minDistanceToEarth;

    public float minSize = 0.1f;
    public float maxSize = 0.7f;
    
    private bool near = false;

    void Start()
    {
        gameController = GameObject.Find("GameController");
    }

    void Update()
    {
        this.Move();

        if(Vector3.Distance(this.transform.position, player.transform.position) < 10f && this.transform.position.z > player.transform.position.z)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Satellite near");
            // -Debug.Break();
            near = true;
        }
        else if(near)
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            near = false;
        }
        else if(this.transform.position.z < player.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        this.transform.LookAt(earth.transform);
        this.transform.RotateAround(earth.transform.position, direction, speed * Time.deltaTime);
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

        gameController = GameObject.Find("GameController");
        float frustumWidth = gameController.GetComponent<GameController>().frustumWidth;
        Vector3 pos = new Vector3(Random.Range(-frustumWidth, frustumWidth), Random.Range(-frustumWidth, frustumWidth), Random.Range(player.transform.position.z + 10, earth.transform.position.z - 3));

        this.direction = new Vector3(Random.value, Random.value, Random.value).normalized;
        speed = Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minSpeed, maxSpeed);

        this.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

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
