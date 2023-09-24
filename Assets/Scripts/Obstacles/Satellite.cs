using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour, IIdleObstacle
{
    private GameObject gameController;
    public GameObject earth;
    public GameObject player;

    public Vector3 direction;

    public float minSpeed = 30f;
    public float maxSpeed = 70f;
    private float speed;

    public float minSize = 0.1f;
    public float maxSize = 0.7f;
    
    private bool near = false;

    private ObjectAudio objectAudio;

    public int damageValue = 1;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        objectAudio = GetComponent<ObjectAudio>();
    }

    void Update()
    {
        this.Move();

        if(this.transform.position.z < player.transform.position.z - 10f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        float frustumWidth = gameController.GetComponent<GameController>().frustumWidth;

        this.transform.LookAt(earth.transform);
        this.transform.RotateAround(earth.transform.position, direction, speed * Time.deltaTime);
    }

    public void FirstMove()
    {
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
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");

        float earthToPlayerDistance = (this.earth.transform.position - this.player.transform.position).magnitude;

        gameController = GameObject.Find("GameController");
        float frustumWidth = gameController.GetComponent<GameController>().frustumWidth;

        Debug.Log(Mathf.Log(player.transform.position.normalized.z + 10));

        float zP = player.transform.position.z - earth.transform.position.z;

        

        Vector3 pos = new Vector3(Random.Range(-frustumWidth, frustumWidth), Random.Range(-frustumWidth, frustumWidth), (Mathf.Sqrt(Random.Range(0f, 1f))) * zP);


        this.direction = new Vector3(Random.value, Random.value, Random.value).normalized;
        speed = Mathf.Sign(Random.Range(-1f, 1f)) * Random.Range(minSpeed, maxSpeed);

        this.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

        return pos;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(damageValue);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            // Audio
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }

    public void setDamageValue(int damageValue)
    {
        this.damageValue = damageValue;
    }
}
