using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class Float : MonoBehaviour
{
    public float forceMax = 1f;
    public float change = 0.01f;
    private int increm = -1;
    private float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, 1, 0) * force * Time.deltaTime);
        transform.Rotate(new Vector3(0, 1, 0) * 50 * Time.deltaTime);

        if (Mathf.Abs(force) > forceMax)
        {
            increm *= -1;
        }
        force += change * increm;
    }
}
