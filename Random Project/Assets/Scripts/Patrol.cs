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

    [SerializeField] private Transform patrolCheckerUnder;
    [SerializeField] private Transform patrolCheckRightLeft;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;

        bool hitHorizontal = Physics2D.Raycast(patrolCheckRightLeft.transform.position, dir, 1,layerMask);
        bool hitUnder = Physics2D.Raycast(patrolCheckerUnder.position, -transform.up, 1, layerMask);
        
        Debug.DrawLine(patrolCheckerUnder.position, patrolCheckerUnder.position + new Vector3(0,-1,0));

        if (hitHorizontal || !hitUnder)
        {
            dir = -dir;
            patrolCheckRightLeft.localScale = new Vector3(-patrolCheckRightLeft.localScale.x, patrolCheckRightLeft.localScale.y);
        }
    }
}
