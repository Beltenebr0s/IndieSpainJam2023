using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private bool startGame = false;
    private bool endGame = false;

    /*
    [Tooltip("Defines the current tracking origin type.")]
    [InspectorName("Quest 1")]
    [RangeAttribute(0.5f, 2.0f)]
    */

    public GameController gameController;

    [Header("Atack Settings")]
    [SerializeField]
    private float minTimeBetweenAttacks = 2;
    [SerializeField]
    private float maxTimeBetweenAttacks = 5;
    private float timeSinceLastAttack = 0f;
    private float timeToNextAttack = 0f;
    [SerializeField]
    private List<GameObject> weaponsList;


    private void Start()
    {
        timeToNextAttack = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            timeSinceLastAttack += Time.deltaTime;
            if(timeSinceLastAttack >= timeToNextAttack)
            {
                //attack();
            }
        }
    }

    public void StartGame()
    {
        startGame = true;
    }

    private void attack()
    {
        timeSinceLastAttack = 0;
        timeToNextAttack = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
        Instantiate(weaponsList[Random.Range(0, weaponsList.Count)]);
    }

    public void EndGame()
    {
        startGame = false;
        endGame = true;
        gameController.EndGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }
}
