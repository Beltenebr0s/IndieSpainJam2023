using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour, IMovingObstacle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(){}

    public void DamagePlayer(int damageValue){}

    public Vector3 FindStartingPosition(){return Vector3.zero;}
}
