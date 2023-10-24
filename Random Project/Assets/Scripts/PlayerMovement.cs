using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPressedRememberTime;
    [SerializeField] private float groundedRememberTime;
    
    private float _movementX;
    private Vector2 _movement;
    private Controls _controls;
    private bool isFlipped;
    private bool canJump = true;

    private float jumpPressedRemember;
    private float groundedRemember;

    private void Awake()
    {
        _controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Player.Move.performed += OnMovementPerformed;
        _controls.Player.Move.canceled += OnMovementCanceled;
    }

    void Update()
    {
        GetPlayerInput();
        MovePlayer();
        HandlePlayerDirection();
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Player.Move.performed -= OnMovementPerformed;
        _controls.Player.Move.canceled -= OnMovementCanceled;
    }

    private void HandlePlayerDirection()
    {
        float flippedValue = transform.localScale.x;
        if (transform.right.x > 0)
        {
            if (_movementX < 0)
            {
                transform.right *= -1;
                // transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                //     transform.localScale.z);
            }
        }
        else
        {
            if (_movementX > 0)
            {
                transform.right *= -1;
                // transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                //     transform.localScale.z);
            }
        }
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        // Debug.Log(obj.ReadValue<Vector2>());
        Vector2 input = obj.ReadValue<Vector2>();
        input.Normalize();
        _movementX = input.x;
        Debug.Log(_movementX);
        if (_movementX < 0)
        {
            _movementX /= _movementX * -1;
        }
        else if (_movementX > 0)
        {
            _movementX /= _movementX;
        }
        else
        {
            return;
        }
    }

    private void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        _movementX = 0;
    }

    private void GetPlayerInput()
    {
        // movementX = Input.GetAxisRaw("Horizontal");
        jumpPressedRemember -= Time.deltaTime;
        groundedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                JumpRemember();
            }
        }
        Jump();
    }

    public void JumpRemember()
    {
        jumpPressedRemember = jumpPressedRememberTime;
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(_movementX * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        bool isGrounded = Physics2D.CircleCast(groundCheck.position, 0.5f, Vector2.down, 0,groundLayer);
        if (isGrounded && canJump)
        {
            Debug.Log("Is grounded");
            groundedRemember = groundedRememberTime;
        }
        if (groundedRemember > 0 && jumpPressedRemember > 0 && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            jumpPressedRemember = 0f;
            groundedRemember = 0f;
            PauseJump(0.2f);
        }
    }

    public IEnumerator PauseJumpCoroutine(float duration)
    {
        canJump = false;
        yield return new WaitForSeconds(duration);
        canJump = true;
    }

    public void PauseJump(float duration)
    {
        StartCoroutine(PauseJumpCoroutine(duration));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
