using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Flashable
{

    public int currentHealth;

    [SerializeField] [Range(1,10)] private int maxHealth;
    [SerializeField] private bool recentlyTookDamage = false;
    [SerializeField] private float takingDamageInvulnerabilityDuration = 10f;

    private bool isDead = false;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerDamaged;
    
    public override void Awake()
    {
        base.Awake();
    }
    
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

    public override void Start()
    {
        base.Start();
        if (HealthManager.instance != null)
        {
            HealthManager.instance.SetNumberOfHearts(currentHealth);
            HealthManager.instance.Init();
            HealthManager.instance.UpdateHearts(currentHealth);
        }
        else
        {
            Debug.LogWarning("There is no HealthManager in this scene.");
        }
    }

    public void TakeDamage(int amount)
    {
        // if the player recently took damage, they are currently invulnerable for some time
        if (recentlyTookDamage)
        {
            return;
        }
        
        currentHealth -= amount;
        OnPlayerDamaged?.Invoke();
        HealthManager.UpdateNumberOfHearts(currentHealth);
        PauseTakingDamage();

        if (currentHealth <= 0 /*&& !isDead*/)
        {
            currentHealth = 0;
            // isDead = true;

            // TODO: kill the player, reset game, etc...
            GetComponent<PlayerMovement>().FreezePlayer(1f);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            OnPlayerDeath?.Invoke();
            Handheld.Vibrate();
            Debug.Log("-- Player died, reloading scene. --");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
