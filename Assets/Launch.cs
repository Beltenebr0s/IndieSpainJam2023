using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;
    private bool firstLaunch = true;
    private bool onPosition = false;
    public void launch()
    {
        if(firstLaunch)
        {
            camera = GameObject.Find("Main Camera");
            camera.GetComponent<CamaraController>().firstLaunch = true;
            firstLaunch = false;
        }
    }
}
