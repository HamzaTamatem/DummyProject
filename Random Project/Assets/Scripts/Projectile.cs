using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed;

    [SerializeField] private float lifetime;
    [SerializeField] private int damage;
    [SerializeField] private GameObject onImpactParticlePrefab;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Move the projectile in the desired direction
        _rigidbody2D.AddForce(direction * speed, ForceMode2D.Impulse);
        
        // Calculate the angle in radians
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle to degrees
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        
        // rotate the projectile to be facing the direction
        transform.rotation = Quaternion.Euler(0,0,angleInDegrees-90);
        
        // destroy the projectile after a certain period of time
        Die(lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
            Instantiate(onImpactParticlePrefab, transform.position, Quaternion.identity);
            Die(0);
        } else if (other.CompareTag("BlockBullet"))
        {
            Instantiate(onImpactParticlePrefab, transform.position, Quaternion.identity);
            Die(0);
        }
        else
        {
            Instantiate(onImpactParticlePrefab, transform.position, Quaternion.identity);
            Die(0);
        }
    }

    public void Die(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DieCoroutine(duration));
    }

    private IEnumerator DieCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
