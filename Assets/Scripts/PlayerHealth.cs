using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject gameController;
    public float maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        gameController = GameObject.Find("GameController");
        InitHealth();
    }

    void Update()
    {}

    public void InitHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageValue)
    {
        Debug.Log("Ouch! (damage: " + damageValue + ")");
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            Debug.Log("Muero");
        }
    }
}
