using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    private float distanceFromPlayer;
    public GameObject player;
    public bool firstLaunch = false;
    private bool onPosition = false;
    private bool firstPosition = false;

    private Vector3 firstPositionVector = new Vector3(7f,0.3f,0f);
    private Vector3 secondPositionVector = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        distanceFromPlayer = this.transform.position.z - player.transform.position.z;
        firstPositionVector.z = player.transform.position.z;
        secondPositionVector = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z - 13);
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
                    onPosition = true;
                }
            }

        }
        if(onPosition){
            secondPositionVector.z = player.transform.position.z - 13;
            this.transform.position = secondPositionVector;
        }

    }
}

