using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject tierra;

    public GameController gameControoler;

    [Header("Movement Settings")]
    public float speed = 1;
    public float maneuverSpeed = 0.1f;
    public float maxManeuverSpeed = 5;
    public float maxSpeed = 50;
    public float takeoffSpeed = 20f;
    public float dashAceleration = 10f;
    public float constantAceleration = 0.5f;
    public Animator playerAnimator;
    [System.NonSerialized]
    public float frustumWidth;

    public bool enCaida = false;

    private Rigidbody playerRB;

    private bool firstLaunch = true;

    [Header("Para debug <3")]
    public float velocity;

    [SerializeField] private EventReference scream;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();  
    }

    public void calculateFrustumWidth(float distanceFromCameraZ)
    {
        // se calcula los limites en los que se puede mover el personaje
        float frustumHeight = 2.0f * distanceFromCameraZ * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = (frustumHeight / Camera.main.aspect)*0.7f;
        gameControoler.frustumWidth = frustumWidth;
    }

    public void launch()
    {
        Debug.LogError("Lanzando");
    }
    

    // Update is called once per frame
    void Update()
    {
        velocity = playerRB.velocity.z;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (firstLaunch && !enCaida && gameControoler.startGame){
                LaunchAnimation();
                firstLaunch = false;
            }
            else if(!enCaida && gameControoler.startGame){
                LaunchAnimation();
                Launch();
            }
        }

        if (enCaida)
        {
            acelerar(constantAceleration * Time.deltaTime);

            // Mira siempre a la tierra
            this.transform.LookAt(tierra.transform.position);

            // comprueba el movimiento que se quiere hacer
            float xMove = Input.GetAxisRaw("Horizontal");
            float yMove = Input.GetAxisRaw("Vertical");
            
            // se calcula la aceleracion sin que supere un maximo
            xMove = playerRB.velocity.x + xMove * maneuverSpeed;
            yMove = playerRB.velocity.y + yMove * maneuverSpeed;
            xMove = Mathf.Clamp(xMove, -maxManeuverSpeed, maxManeuverSpeed);
            yMove = Mathf.Clamp(yMove, -maxManeuverSpeed, maxManeuverSpeed);

            // se asigna la aceleracion
            playerRB.velocity = new Vector3(xMove, yMove, playerRB.velocity.z) * speed;

            // se comprueba si se traspasan los limites
            Vector3 limPos = transform.position;
            limPos.x = Mathf.Clamp(limPos.x, -frustumWidth, frustumWidth);
            limPos.y = Mathf.Clamp(limPos.y, -frustumWidth, frustumWidth);

            // se corrige en caso de sobrepasarlos
            if (Mathf.Abs(this.transform.position.x - limPos.x) > 0.001f)
            {
                if ((playerRB.velocity.x > 0 && limPos.x > 0) || (playerRB.velocity.x < 0 && limPos.x < 0))
                {
                    playerRB.velocity = new Vector3(0, playerRB.velocity.y, playerRB.velocity.z);
                    this.transform.position = new Vector3(limPos.x, this.transform.position.y, this.transform.position.z);
                }

            }
            if (Mathf.Abs(this.transform.position.y - limPos.y) > 0.001f)
            {
                if ((playerRB.velocity.y > 0 && limPos.y > 0) || (playerRB.velocity.y < 0 && limPos.y < 0))
                {
                    playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
                    this.transform.position = new Vector3(this.transform.position.x, limPos.y, this.transform.position.z);
                }

            }

        }
    }

    public void Reset()
    {
        playerRB.velocity = Vector3.zero;
        enCaida = false;
    }
    public void LaunchAnimation()
    {
        gameControoler.RestartGame();
        playerAnimator.SetTrigger("LaunchPlayer");
    }

    public void Launch()
    {
        enCaida = true;
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, takeoffSpeed);
        RuntimeManager.PlayOneShot(scream);
    }

    public void acelerar(float speedAceleration)
    {
        float zVelocity = Mathf.Clamp(playerRB.velocity.z + speedAceleration, 0, maxSpeed);
        playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, zVelocity);
    }

    public void hitPlayer(float force)
    {
        playerAnimator.SetTrigger("DamagePlayer");
        acelerar(-force * 2);
    }

    public void EndGame()
    {
        enCaida = false;
        playerRB.velocity = Vector3.zero;
    }

}
