using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Patrol : MonoBehaviour
{
    [Tooltip("The starting direction of the game object that is patrolling.")]
    [SerializeField] private Vector2 dir;
    [Tooltip("Speed of patrolling.")]
    [SerializeField] private float speed;
    [Tooltip("Game object will switch directions when hitting this layer.")]
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;

        bool hit = Physics2D.Raycast(transform.GetChild(0).transform.position, dir, 1,layerMask);

        if (hit)
        {
            dir = -dir;
            transform.GetChild(0).transform.localScale = new Vector3(-transform.GetChild(0).transform.localScale.x, transform.GetChild(0).transform.localScale.y);
        }
    }
}
