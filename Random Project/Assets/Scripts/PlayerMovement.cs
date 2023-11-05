using System;
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
    [SerializeField] [Range(0,1)] private float horizontalDamping;
    
    
    private float _movementX;
    private Vector2 _movement;
    private Controls _controls;
    private bool isFlipped;
    private bool canJump = true;

    private bool isMoving; // to store if player is clicking any buttons at the moment or not

    private float jumpPressedRemember;
    private float groundedRemember;

    public bool pauseMovement;
    public static bool pauseInput;

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
        MoveButton.OnMovePressed += Move;
    }

    private void FixedUpdate()
    {
        if (!pauseMovement)
        {
            MovePlayer();
        }
    }

    void Update()
    {
        jumpPressedRemember -= Time.deltaTime;
        groundedRemember -= Time.deltaTime;
        
        GetPlayerInput();
        // MovePlayer();
        HandlePlayerDirection();
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Player.Move.performed -= OnMovementPerformed;
        _controls.Player.Move.canceled -= OnMovementCanceled;
        MoveButton.OnMovePressed -= Move;
    }

    public void Move(float inputX)
    {
        _movementX = inputX;
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

    public void Move(Vector2 input)
    {
        _movementX = input.x;
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
        // Debug.Log(_movementX);
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
        // get movement input from movement buttons
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                JumpRemember();
            }
        }
#endif
        TryJump();
    }

    public void JumpRemember()
    {
        jumpPressedRemember = jumpPressedRememberTime;
    }

    private void MovePlayer()
    {
        // float horizontalVelocity = rb.velocity.x;
        // horizontalVelocity += _movementX;
        // horizontalVelocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10);
        // rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        rb.velocity = new Vector2(_movementX * moveSpeed, rb.velocity.y);
    }

    public void TryJump()
    {
        // ground check
        bool isGrounded = Physics2D.CircleCast(groundCheck.position, 0.5f, Vector2.down, 0,groundLayer);
        if (isGrounded && canJump)
        {
            // Debug.Log("Is grounded");
            groundedRemember = groundedRememberTime;
        }
        if (groundedRemember > 0 && jumpPressedRemember > 0 && canJump)
        {
            jumpPressedRemember = 0f;
            groundedRemember = 0f;
            
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            
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
    
    private IEnumerator PausePlayerMovementCoroutine(float duration)
    {
        // Debug.Log("Movement is paused.");
        pauseMovement = true;
        yield return new WaitForSeconds(duration);
        // Debug.Log("Movement is resumed.");
        pauseMovement = false;
    }

    public void PausePlayerMovement(float duration)
    {
        StartCoroutine(PausePlayerMovementCoroutine(duration));
    }

    public void PushPlayerAwayFrom(Vector3 position, float forceMagnitude)
    {
        PausePlayerMovement(0.5f);
        Vector2 directionVector = transform.position - position;
        rb.velocity = Vector2.zero;
        rb.AddForce(directionVector.normalized * forceMagnitude, ForceMode2D.Impulse);
    }

    private IEnumerator FreezePlayerCoroutine(float duration)
    {
        float originalSpeed = moveSpeed;
        
        _controls.Disable();
        pauseInput = true;
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        _controls.Enable();
        pauseInput = false;
        moveSpeed = originalSpeed;
    }

    public void FreezePlayer(float duration)
    {
        StartCoroutine(FreezePlayerCoroutine(duration));
    }
}
