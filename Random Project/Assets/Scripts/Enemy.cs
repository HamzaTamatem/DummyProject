using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float damage;
    public abstract void GetHit(float amount);

    public void DoSomething()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
