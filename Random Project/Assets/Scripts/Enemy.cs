using System;
using UnityEngine;

[RequireComponent(typeof(GiveDamage))]
public abstract class Enemy : Flashable
{
    public float damage;
    public int currentHealth;
    
    [SerializeField] private int maxHealth;
    // public abstract void GetHit(int amount);

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        Debug.Log(nameof(TakeDamage));
        currentHealth -= amount;
        Debug.Log($"Current health is: {currentHealth}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
