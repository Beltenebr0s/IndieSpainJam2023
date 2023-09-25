using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public GameObject player;
    public bool firstLaunch = false;
    private bool onPosition = false;
    private bool firstPosition = false;

    public float distanceFromPlayer = 13f;

    private Vector3 firstPositionVector = new Vector3(7f,0.3f,0f);
    private Vector3 secondPositionVector = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        firstPositionVector.z = player.transform.position.z;
        secondPositionVector = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z - distanceFromPlayer);

        player.GetComponent<PlayerController>().calculateFrustumWidth(distanceFromPlayer);
    }

    // Update is called once per frame
    void Update()
    {        
        float duration1;
        float duration2 = 8f;
        duration1 = (Vector3.Distance(secondPositionVector, firstPositionVector) * duration2)/Vector3.Distance(this.transform.position, firstPositionVector) ;

        float time = 0; 

        if(firstLaunch && !onPosition)
        {
            this.transform.LookAt(player.transform);
            if(!firstPosition)
            {
                time += Time.deltaTime;
                this.transform.position = Vector3.Lerp(this.transform.position, firstPositionVector, duration1 * Time.deltaTime);

                if(Vector3.Distance(this.transform.position, firstPositionVector) < 2f)
                {
                    firstPosition = true;
                }
            }
            else
            {
                this.transform.position = Vector3.Lerp(this.transform.position, secondPositionVector, duration2 * Time.deltaTime);
                duration2 += 0.3f;
                if(Vector3.Distance(this.transform.position, secondPositionVector) < 0.1f)
                {
                    this.transform.position = secondPositionVector;
                    player.GetComponent<PlayerController>().Launch();
                    onPosition = true;
                }
            }

        }
        if(onPosition){
            secondPositionVector.z = player.transform.position.z - distanceFromPlayer;
            this.transform.position = secondPositionVector;
        }

        float frustumHeight = 2.0f * distanceFromPlayer * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight / Camera.main.aspect;

        Debug.DrawLine(new Vector3(-frustumWidth, -frustumWidth, player.transform.position.z), new Vector3(frustumWidth, -frustumWidth, player.transform.position.z), Color.red);
        Debug.DrawLine(new Vector3(-frustumWidth, frustumWidth, player.transform.position.z), new Vector3(frustumWidth, frustumWidth, player.transform.position.z), Color.red);
        Debug.DrawLine(new Vector3(-frustumWidth, -frustumWidth, player.transform.position.z), new Vector3(-frustumWidth, frustumWidth, player.transform.position.z), Color.red);
        Debug.DrawLine(new Vector3(frustumWidth, -frustumWidth, player.transform.position.z), new Vector3(frustumWidth, frustumWidth, player.transform.position.z), Color.red);

    }
}

