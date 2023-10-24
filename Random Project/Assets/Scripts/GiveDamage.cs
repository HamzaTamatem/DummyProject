using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(damage);
        }
    }
}
