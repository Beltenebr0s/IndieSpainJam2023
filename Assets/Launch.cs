using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    private GameObject _camera;
    public void launch()
    {
        _camera = GameObject.Find("Main Camera");
        _camera.GetComponent<CamaraController>().isLaunching = true;

    }
}
