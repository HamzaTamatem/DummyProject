using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpPressedRememberTime;
    [SerializeField] private float groundedRememberTime;
    [SerializeField] [Range(0,1)] private float horizontalDamping;
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float distanceToTriggerWallSlide;
    [SerializeField] private float normalMoveSpeed = 5f;

    [Header("Dash")] 
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashRemember;
    [SerializeField] private float dashSpeed;
    private float dashTimer;
    private SpriteHandler spriteHandler;

    private Coroutine waitUntilLand;


    private float _movementX;
    private Vector2 _movement;
    private Controls _controls;
    private bool isFlipped;
    private bool canJump = true;
    private bool isDashing;
    [SerializeField] private bool canLandOnce;

    private bool isMoving; // to store if player is clicking any buttons at the moment or not

    private float jumpPressedRemember;
    private float groundedRemember;

    public bool pauseMovement;
    public static bool pauseInput;
    public bool isGrounded;
    public bool canWallSlide;
    public bool isWallSliding;
    
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    ParticleSystem jumpParticles;
    ParticleSystem slideParticles;

    private void Awake()
    {
        _controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        spriteHandler = GetComponentInChildren<SpriteHandler>();

        jumpParticles = transform.Find("Jump Particles").GetComponent<ParticleSystem>();
        slideParticles = transform.Find("Slide Particles").GetComponent<ParticleSystem>();

        moveSpeed = normalMoveSpeed;
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
        
        if (dashTimer > 0)
            dashTimer -= Time.deltaTime;
        
        GetPlayerInput();
        // MovePlayer();
        HandlePlayerDirection();

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Dash();
        }
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

        if (isDashing)
        {
            if (_movementX == 0)
            {
                _movementX = transform.right.x;
            }
        }
        
        rb.velocity = new Vector2(_movementX * moveSpeed, rb.velocity.y);

        if(/*!canLandOnce &&*/ isGrounded && !isWallSliding && !isDashing)
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
        //Debug.DrawLine(transform.position,transform.position + (distanceToTriggerWallSlide*transform.right),Color.blue);

        // if the player is falling, then we can check for a wall slide
        if (rb.velocity.y < 0 && canWallSlide)
        {
            //Debug.Log("WALLSLIDING!");
            // rb.AddForce(Vector2.up * wallSlidingSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            isWallSliding = true;

            spriteHandler.ChangeAnim(SpriteHandler.Anim.Slide);
            var SP = slideParticles.emission;
            SP.rateOverDistance = 10;

            //slideParticles.Play();
        }
        else
        {
            //Debug.Log("NOT WALLSLIDING!");
            isWallSliding = false;
        }

        if (spriteHandler.currentAnim == SpriteHandler.Anim.Slide && !isWallSliding)
        {
            spriteHandler.ChangeAnim(SpriteHandler.Anim.Idle);
            var SP = slideParticles.emission;
            SP.rateOverDistance = 0;
            //print("Not wall slideing");
        }
    }

    //IEnumerator WaitUntilLand()
    //{
    //    if (canJump)
    //        yield break;

    //    yield return new WaitForSeconds(0.1f);
    //    yield return new WaitUntil(() => isGrounded);
    //    //print("Is grounded");
    //}

    public void TryJump()
    {
        // ground check
        isGrounded = Physics2D.CircleCast(groundCheck.position, 0.5f, Vector2.down, 0,groundLayer);

        if (isGrounded)
        {
            if (groundedRemember <= 0 && canLandOnce)
            {
                //print("Land");
                //Change the animation to Land
                spriteHandler.ChangeAnim(SpriteHandler.Anim.Land);
                jumpParticles.Play();
                canLandOnce = false;
            }
        }

        if (isGrounded || isWallSliding && canJump)
        {
            // Debug.Log("Is grounded");
            groundedRemember = groundedRememberTime;
        }
        else
        {
            if(!isWallSliding)
            {
                if (spriteHandler.currentAnim != SpriteHandler.Anim.Jump)
                {
                    //Change the animation to Fall
                    spriteHandler.ChangeAnim(SpriteHandler.Anim.Fall);
                    //StartCoroutine(WaitUntilLand());

                    if (!canLandOnce)
                    {
                        canLandOnce = true;
                    }
                }
            }
        }
        if (groundedRemember > 0 && jumpPressedRemember > 0 && canJump)
        {
            jumpPressedRemember = 0f;
            groundedRemember = 0f;

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);

            //canLandOnce = true;

            //Change the animation to Jump
            spriteHandler.ChangeAnim(SpriteHandler.Anim.Jump);

            jumpParticles.Play();

            var SP = slideParticles.emission;
            SP.rateOverDistance = 0;

            PauseJump(0.2f);
        }
    }

    public IEnumerator PauseJumpCoroutine(float duration)
    {
        canJump = false;
        yield return new WaitForSeconds(duration);
        canLandOnce = true;
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

    private IEnumerator DashCoroutine()
    {
        float originalGravityScale = rb.gravityScale;
        if (dashTimer <= 0)
        {
            spriteHandler.ChangeAnim(SpriteHandler.Anim.DashStart);
            dashTimer = dashCooldown;
            isDashing = true;
            moveSpeed = dashSpeed;
            _movementX = transform.right.x;
            
            // stop gravity
            rb.gravityScale = 0;
            
            // stop motion in Y
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            
            yield return new WaitForSeconds(dashTime);
            spriteHandler.ChangeAnim(SpriteHandler.Anim.DashEnd);
            moveSpeed = normalMoveSpeed;
            if (MoveButton.xMovementButtonHeld)
                _movementX = transform.right.x;
            else
                _movementX = 0f;
            isDashing = false;
            rb.gravityScale = originalGravityScale;
        }
    }

    public void Dash()
    {
        Debug.Log(nameof(Dash));
        StartCoroutine(DashCoroutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (distanceToTriggerWallSlide * transform.right));
    }
}
