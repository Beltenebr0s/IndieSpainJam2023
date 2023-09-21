using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    private float distanceFromPlayer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlayer = this.transform.position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + distanceFromPlayer);
    }
}
