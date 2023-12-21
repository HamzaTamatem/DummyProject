using UnityEngine;

public class FallTrap : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] float speed;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed;
    }
}
