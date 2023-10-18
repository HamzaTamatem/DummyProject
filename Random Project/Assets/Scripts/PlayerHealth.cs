using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float currentHealth;

    [SerializeField] private float maxHealth;

    public static event Action OnPlayerDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            // TODO: kill the player, reset game, etc...
            OnPlayerDeath?.Invoke();
        }

        Debug.Log($"The current health of the player is: {currentHealth}");
    }
}
