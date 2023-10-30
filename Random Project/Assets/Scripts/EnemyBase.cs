public class EnemyBase : Enemy
{
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
