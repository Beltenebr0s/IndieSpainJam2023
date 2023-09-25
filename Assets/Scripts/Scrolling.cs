using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{

    [SerializeField] private RawImage image;
    [SerializeField] private float x, y;
    [SerializeField] private bool reversedInPause;

    private float dt, xmove, ymove;
    void Update()
    {
        dt = Time.unscaledDeltaTime;

        if (Time.timeScale == 0 && reversedInPause)
        {
            xmove = image.uvRect.position.x - x * dt;
            ymove = image.uvRect.position.y + y * dt;
        }
        else
        {
            xmove = image.uvRect.position.x + x * dt;
            ymove = image.uvRect.position.y + y * dt;
        }
        image.uvRect = new Rect(xmove, ymove, image.uvRect.size.x, image.uvRect.size.y);
    }
}
