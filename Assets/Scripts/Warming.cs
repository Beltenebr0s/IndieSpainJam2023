using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warming : MonoBehaviour
{
    //public Re
    public GameObject warmUi;
    private bool warmUiActive = false;

    private GameObject canvas;
    private GameObject player; 
    Vector2 screenPosition;
    float frustumWidth;
    float frustumHeight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        canvas = GameObject.Find("Canvas");
        //warmUi.SetActive(false);
    }
    bool noBreak = false;
    // Update is called once per frame
    void Update()
    {
        

        float distanceFromCameraZ = Vector3.Distance(new Vector3(0, 0, player.transform.position.z), Camera.main.transform.position);
        frustumHeight = 2.0f * distanceFromCameraZ * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight / Camera.main.aspect;
        Vector3 frustum = Camera.main.WorldToScreenPoint(new Vector3(frustumWidth, frustumWidth, 0));
        Vector3 obstacleOnScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);


        bool isOnScreen = (obstacleOnScreenPos.x < frustum.y && obstacleOnScreenPos.x > -frustum.y && obstacleOnScreenPos.y < frustum.y && obstacleOnScreenPos.y > -frustum.y);

        // if the distance with the player is lower than 10, show the warming
        if (!isOnScreen && Vector3.Distance(this.transform.position, player.transform.position) < 40 && Mathf.Abs(this.transform.position.z - player.transform.position.z) < 20)
        {
            if(!warmUiActive){
                warmUi = Instantiate(warmUi, canvas.transform);
                warmUiActive = true;
            }

            //warmUi.SetActive(true);
            

            screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

            float imageSize = warmUi.GetComponent<RectTransform>().rect.width / 2;

            screenPosition.x = Mathf.Clamp(screenPosition.x, frustum.y + imageSize, frustum.y * 3.0f - imageSize);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 0 + imageSize, frustum.y * 2.0f - imageSize);

            float rot_z = Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg;
            warmUi.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            warmUi.transform.position = screenPosition;
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;

            if(!noBreak){
                noBreak = true;
            }
        }
        else{
            if(warmUiActive){
                Destroy(warmUi);
                Destroy(this);

            }
        }
    }
}
