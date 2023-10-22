using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Flashable
{

    public float currentHealth;

    [SerializeField] private float maxHealth;
    [SerializeField] private bool recentlyTookDamage = false;
    [SerializeField] private float takingDamageInvulnerabilityDuration = 10f;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerDamaged;

    private void OnEnable()
    {
        OnPlayerDamaged += Flash;

        currentHealth = maxHealth;
        Debug.Log($"Player started with health: {currentHealth}");
    }
    
    private void OnDisable()
    {
        OnPlayerDamaged -= Flash;
    }

    public void TakeDamage(float amount)
    {
        // if the player recently took damage, they are currently invulnerable for some time
        if (recentlyTookDamage)
        {
            return;
        }
        
        currentHealth -= amount;
        OnPlayerDamaged?.Invoke();
        PauseTakingDamage();

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            // TODO: kill the player, reset game, etc...
            OnPlayerDeath?.Invoke();
            Debug.Log("-- Player died, reloading scene. --");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        Debug.Log($"The current health of the player is: {currentHealth}");
    }

    private IEnumerator PauseTakingDamageCoroutine()
    {
        recentlyTookDamage = true;
        yield return new WaitForSeconds(takingDamageInvulnerabilityDuration);
        recentlyTookDamage = false;
    }

    public void PauseTakingDamage()
    {
        StartCoroutine(PauseTakingDamageCoroutine());
    }
    
    
}
