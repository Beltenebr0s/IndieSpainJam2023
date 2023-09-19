using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;

    public Vector3 direction;
    private float minDistanceToEarth;

    void Start()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        this.minDistanceToEarth = 0.5f;
        this.transform.position = this.FindStartingPosition();
    }

    
    void Update()
    {
        //Debug.Log("Distance: " + (this.earth.transform.position - this.transform.position).magnitude);
        this.Move();
    }

    public void Move()
    {
        this.transform.LookAt(earth.transform);
        this.transform.RotateAround(earth.transform.position, direction, 50 * Time.deltaTime);
    }

    public void DamagePlayer(int damageValue){}

    public Vector3 FindStartingPosition()
    {
        float earthToPlayerDistance = (this.earth.transform.position - this.player.transform.position).magnitude;
        
        Vector3 pos = Random.insideUnitSphere * earthToPlayerDistance + 
                    new Vector3(this.minDistanceToEarth, this.minDistanceToEarth, this.minDistanceToEarth);
        
        this.direction = new Vector3(Random.value, Random.value, Random.value).normalized;
        
        return pos;
    }
}
