using UnityEngine;

[RequireComponent(typeof(GiveDamage))]
public abstract class Enemy : Flashable
{
    public int damage;
    public int currentHealth;
    
    [SerializeField] private int maxHealth;

    [SerializeField] private GameObject deathParticles;
    // public abstract void GetHit(int amount);

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        Debug.Log(nameof(TakeDamage));
        currentHealth -= amount;
        
        Flash();
        Debug.Log($"Current health is: {currentHealth}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    public void ProduceParticles()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
