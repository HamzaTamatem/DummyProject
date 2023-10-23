using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
        }
    }
}
