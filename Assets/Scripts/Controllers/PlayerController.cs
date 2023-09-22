using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject tierra;

    public GameController gameControoler;

    [Header("Movement Settings")]
    public float speed = 1;
    public float maxSpeed = 50;
    public float takeoffSpeed = 20f;
    public float dashAceleration = 10f;
    public float constantAceleration = 0.5f;

    [System.NonSerialized]
    public float frustumWidth;

    public bool enCaida = false;

    private Rigidbody playerRB;

    [Header("Para debug <3")]
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();

        // se calcula los limites en los que se puede mover el personaje
        float distanceFromCameraZ = Vector3.Distance(new Vector3(0, 0, this.transform.position.z), Camera.main.transform.position);
        float frustumHeight = 2.0f * distanceFromCameraZ * Mathf.Tan(Camera.main.fieldOfView * 0.4f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight / Camera.main.aspect;
        gameControoler.frustumWidth = frustumWidth;
    }
    

    // Update is called once per frame
    void Update()
    {
        velocity = playerRB.velocity.z;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!enCaida)
                lanzarse();
        }

        if (enCaida)
        {
            acelerar(constantAceleration * Time.deltaTime);

            // Mira siempre a la tierra
            this.transform.LookAt(tierra.transform.position);

            // comprueba el movimiento que se quiere hacer
            float xMove = Input.GetAxisRaw("Horizontal");
            float yMove = Input.GetAxisRaw("Vertical");

            // se a�ade la aceleracion sin que supere un maximo
            float normSpeed = 0.01f;
            if (Mathf.Abs(playerRB.velocity.x + xMove * normSpeed) < maxSpeed)
                xMove = playerRB.velocity.x + xMove * normSpeed;
            else
                xMove = maxSpeed;
            if (Mathf.Abs(playerRB.velocity.y + yMove * normSpeed) < maxSpeed)
                yMove = playerRB.velocity.y + yMove * normSpeed;
            else
                yMove = maxSpeed;

            // se asigna la aceleracion
            playerRB.velocity = new Vector3(xMove, yMove, playerRB.velocity.z) * speed;

            // se comprueba si se traspasan los limites
            Vector3 limPos = transform.position;
            limPos.x = Mathf.Clamp(limPos.x, -frustumWidth, frustumWidth);
            limPos.y = Mathf.Clamp(limPos.y, -frustumWidth, frustumWidth);

            // se corrige en caso de sobrepasarlos
            if (Mathf.Abs(this.transform.position.x - limPos.x) > 0.1f)
                if ((playerRB.velocity.x > 0 && limPos.x > 0) || (playerRB.velocity.x < 0 && limPos.x < 0))
                    playerRB.velocity = new Vector3(0, playerRB.velocity.y, playerRB.velocity.z);
            if (Mathf.Abs(this.transform.position.y - limPos.y) > 0.1f)
                if ((playerRB.velocity.y > 0 && limPos.y > 0) || (playerRB.velocity.y < 0 && limPos.y < 0))
                    playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);

        }
    }

    public void lanzarse()
    {
        gameControoler.StartGame();
        enCaida = true;
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, takeoffSpeed);
    }

    public void acelerar(float speedAceleration)
    {
        float zVelocity = Mathf.Clamp(playerRB.velocity.z + speedAceleration, 0, maxSpeed);
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, playerRB.velocity.z + speedAceleration);
    }

    public void hitPlayer(float force)
    {
        acelerar(-force * 2);
    }

    public void EndGame()
    {
        enCaida = false;
        playerRB.velocity = Vector3.zero;
    }

    public void OnDestroy()
    {
        AudioParameters.Death = 1;
    }
}
