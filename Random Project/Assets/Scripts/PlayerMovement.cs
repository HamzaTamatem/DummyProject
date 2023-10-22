using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    private float movementX;
    private Vector2 movement;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += OnMovementPerformed;
        controls.Player.Move.canceled += OnMovementCanceled;
    }

    void Update()
    {
        GetPlayerInput();
        MovePlayer();
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Move.performed -= OnMovementPerformed;
        controls.Player.Move.canceled -= OnMovementCanceled;
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        // Debug.Log(obj.ReadValue<Vector2>());
        movementX = obj.ReadValue<Vector2>().x;
    }

    private void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        movementX = 0;
    }

    private void GetPlayerInput()
    {
        // movementX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(movementX * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        bool isGrounded = Physics2D.CircleCast(groundCheck.position, 0.5f, Vector2.down, 0,groundLayer);
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);

        }
        // if (rb.velocity.y == 0)
        // {
        //     rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
        // }
    }

    private void OnValidate()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
