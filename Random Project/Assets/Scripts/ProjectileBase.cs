using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] public float speed, currentSpeed;
    [HideInInspector] public bool aim;

    [SerializeField] bool dontDesOnTouchGround;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    void FixedUpdate()
    {
        if (aim)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 difference = player.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ + -90);
            aim = false;
        }

        if (currentSpeed < speed)
        {
            currentSpeed += 0.3f;
        }

        rb.velocity = transform.up * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6 && !dontDesOnTouchGround)
        {
            Destroy(gameObject);
        }
    }
}
