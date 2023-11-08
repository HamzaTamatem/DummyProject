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
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float distanceToTriggerWallSlide;
    private SpriteHandler spriteHandler;


    private float _movementX;
    private Vector2 _movement;
    private Controls _controls;
    private bool isFlipped;
    private bool canJump = true;
    [SerializeField] private bool canLandOnce;

    private bool isMoving; // to store if player is clicking any buttons at the moment or not

    private float jumpPressedRemember;
    private float groundedRemember;

    public bool pauseMovement;
    public static bool pauseInput;
    public bool isGrounded;
    public bool canWallSlide;
    public bool isSliding;

    private void Awake()
    {
        _controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        spriteHandler = GetComponentInChildren<SpriteHandler>();
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
            TryWallSlide();
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
        if (pauseInput) return;
        
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

        if(!canLandOnce && isGrounded && !isSliding)
        {
            if (MathF.Abs(_movementX) > 0)
            {
                //Change the animation to Run
                spriteHandler.ChangeAnim(SpriteHandler.Anim.Run);
            }
            else
            {
                //Change the animation to Idle
                spriteHandler.ChangeAnim(SpriteHandler.Anim.Idle);
            }
        }
    }

    public void TryWallSlide()
    {
        canWallSlide = Physics2D.Raycast(transform.position, transform.right, distanceToTriggerWallSlide, groundLayer);
        Debug.DrawLine(transform.position,transform.position + (distanceToTriggerWallSlide*transform.right),Color.blue);

        // if the player is falling, then we can check for a wall slide
        if (rb.velocity.y < 0 && canWallSlide)
        {
            //Debug.Log("WALLSLIDING!");
            // rb.AddForce(Vector2.up * wallSlidingSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            isSliding = true;
        }
        else
        {
            //Debug.Log("NOT WALLSLIDING!");
            isSliding = false;
        }
    }

    public void TryJump()
    {
        // ground check
        isGrounded = Physics2D.CircleCast(groundCheck.position, 0.5f, Vector2.down, 0,groundLayer);

        if (isGrounded && canJump)
        {
            if (groundedRemember <= 0 && canLandOnce)
            {
                //Change the animation to Land
                spriteHandler.ChangeAnim(SpriteHandler.Anim.Land);
                canLandOnce = false;
            }

            // Debug.Log("Is grounded");
            groundedRemember = groundedRememberTime;
        }
        else
        {
            if(isSliding)
            {
                spriteHandler.ChangeAnim(SpriteHandler.Anim.Slide);
            }
            else
            {
                if(spriteHandler.currentAnim != SpriteHandler.Anim.Jump)
                {
                    //Change the animation to Fall
                    spriteHandler.ChangeAnim(SpriteHandler.Anim.Fall);
                }
            }
        }
        if (groundedRemember > 0 && jumpPressedRemember > 0 && canJump)
        {
            jumpPressedRemember = 0f;
            groundedRemember = 0f;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);

            canLandOnce = true;

            //Change the animation to Jump
            spriteHandler.ChangeAnim(SpriteHandler.Anim.Jump);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (distanceToTriggerWallSlide * transform.right));
    }
}
