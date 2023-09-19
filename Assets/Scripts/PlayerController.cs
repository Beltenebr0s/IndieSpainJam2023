using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 1;
    public float maxSpeed = 50;

    private Rigidbody playerRB;
    private Vector3 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
        

        float bounds;
        if (Screen.width <= Screen.height)
            bounds = Screen.width;
        else
            bounds = Screen.height;
        screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(bounds, bounds));
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        float normSpeed = 0.01f;

        if (Mathf.Abs(playerRB.velocity.x + xMove * normSpeed) < maxSpeed)
            xMove = playerRB.velocity.x + xMove * normSpeed;
        else
            xMove = maxSpeed;

        if (Mathf.Abs(playerRB.velocity.y + yMove * normSpeed) < maxSpeed)
            yMove = playerRB.velocity.y + yMove * normSpeed;
        else
            yMove = maxSpeed;

        playerRB.velocity = new Vector3(xMove, yMove, playerRB.velocity.z) * speed;


        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(pos), new Vector3(0,0,-28.17f),Color.red);
        Vector3 limPos = Camera.main.ViewportToWorldPoint(pos);
        
        if (Mathf.Abs(this.transform.position.x - limPos.x) > 0.1f)
            if ((playerRB.velocity.x > 0 && limPos.x > 0) || (playerRB.velocity.x < 0 && limPos.x < 0))
                playerRB.velocity = new Vector3(0, playerRB.velocity.y, playerRB.velocity.z);
        if (Mathf.Abs(this.transform.position.y - limPos.y) > 0.1f)
            if ((playerRB.velocity.y > 0 && limPos.y > 0) || (playerRB.velocity.y < 0 && limPos.y < 0))
                playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
    }
}
