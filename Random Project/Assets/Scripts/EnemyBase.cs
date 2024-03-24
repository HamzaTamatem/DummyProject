using System;

public class EnemyBase : Enemy
{
    public static event Action OnEnemyDie;

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (currentHealth <= 0)
        {
            OnEnemyDie?.Invoke();
            FindObjectOfType<AudioManager>().Play("EnemyDes");
            Destroy(gameObject);
            ProduceParticles();
        }
    }

}
