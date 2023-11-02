using UnityEngine;

public class OnTriggerEnemy : MonoBehaviour
{

    [SerializeField] private int damageToEnemy;
    [SerializeField] private bool oldEnemyScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Debug.Log("Entered trigger collision with an enemy");
            if (oldEnemyScript)
            {
                if (other.TryGetComponent(out Enemy_Base enemyBase))
                {
                    enemyBase.GetHit(damageToEnemy);
                }
            }
            else
            {
                if (other.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damageToEnemy);
                }
            }
        }
    }
}
