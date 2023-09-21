using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour, IIdleObstacle
{
    public GameObject earth;
    public GameObject player;

    private float minDistanceToEarth;

    public float forceMax = 1f;
    public float change = 0.01f;
    private int increm = -1;
    public float force;

    void Update()
    {
        this.Move();
    }

    public void Move()
    {
        this.transform.Translate(new Vector3(0, 1, 0) * force * Time.deltaTime);

        if (Mathf.Abs(force) > forceMax)
        {
            increm *= -1; 
        }
        force += change * increm;
    }

    public void DamagePlayer(int damageValue){}

    public Vector3 FindStartingPosition()
    {
        earth = GameObject.Find("Earth");
        player = GameObject.Find("Player");
        this.minDistanceToEarth = 0.5f;
        force = forceMax;
        Vector3 earthToPlayerVector = this.earth.transform.position - this.player.transform.position;

        Vector3 pos = Vector3.one * this.minDistanceToEarth
                     + Random.Range (0, 1) * earthToPlayerVector 
                     + Vector3.right * Random.Range(-10, 10);
        
        return pos;
    }
}
