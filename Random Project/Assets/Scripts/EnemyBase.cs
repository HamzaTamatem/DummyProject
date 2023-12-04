public class EnemyBase : Enemy
{
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDes");
            Destroy(gameObject);
            ProduceParticles();
        }
    }

}
