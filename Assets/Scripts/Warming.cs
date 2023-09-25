using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warming : MonoBehaviour
{
    //public Re
    public GameObject warmUiPref;
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
        float distanceFromCameraZ = Vector3.Distance(new Vector3(0, 0, this.transform.position.z), Camera.main.transform.position);
        frustumHeight = 2.0f * distanceFromCameraZ * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight / Camera.main.aspect;
        
        Vector3 frustum = Camera.main.WorldToScreenPoint(new Vector3(frustumHeight, frustumWidth, this.transform.position.z));

        Vector3 obstacleOnScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        float resto = (Camera.main.pixelWidth - Camera.main.pixelHeight) * 3 / 8;
        bool isOnScreen = resto < obstacleOnScreenPos.x && obstacleOnScreenPos.x < Camera.main.pixelWidth - resto && 0 < obstacleOnScreenPos.y && obstacleOnScreenPos.y < Camera.main.pixelHeight;
        
        if (!isOnScreen && Vector3.Distance(this.transform.position, player.transform.position) < 20 && 
            this.transform.position.z >= player.transform.position.z)
        {
            if(!warmUiActive){
                warmUi = Instantiate(warmUiPref, canvas.transform);
                warmUiActive = true;
            }            

            screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);

            float imageSize = warmUi.GetComponent<RectTransform>().rect.width / 2;

            screenPosition.x = Mathf.Clamp(screenPosition.x, resto + imageSize, Camera.main.pixelWidth - resto - imageSize);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 0 + imageSize, Camera.main.pixelHeight - imageSize);


            float rot_z = Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg;
            warmUi.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            warmUi.transform.position = screenPosition;
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else{
            if(warmUiActive){
                Destroy(warmUi);
                Destroy(this);

            }
        }
    }

    private void OnDestroy() {
        
        Destroy(warmUi);
        Destroy(this);
    }
}
