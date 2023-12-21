using System;
using System.Collections;
using UnityEngine;

public class Boss_1 : Enemy
{
    enum State { Idle, Jump, Dash, HitGround,None }
    State state;
    [SerializeField] State testState;
    [SerializeField] bool testMode;

    Transform playerPos;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [Header("Action Time")]
    [SerializeField] float idleTime;
    float currentIdleTime;
    float currentActionTime;
    [SerializeField] float jumpTime;
    [SerializeField] float dashTime;
    [SerializeField] float hitGroundTime;

    [Header("Attriutes")]
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float dashForce;
    [SerializeField] float hitGroundJumpForce;
    [SerializeField] float hitGroundWaitTime;

    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject[] projectileMachines;

    Vector2 direction;
    Vector2 firstPos;

    float groundCheckDistance;
    float wallCheckDistance;

    bool startProjectileMachine;
    bool stopHitGround;
    bool stopSetValue;

    [SerializeField] int[] actionSpamNumber;

    BossSpriteHandler spriteHandler;

    bool startState;

    [SerializeField] GameObject flashImg;

    public override void Awake()
    {
        base.Awake();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        direction.x = -1;

        groundCheckDistance = 0.1f;
        wallCheckDistance = 1.7f;

        actionSpamNumber = new int[Enum.GetValues(typeof(State)).Length];

        spriteHandler = GetComponentInChildren<BossSpriteHandler>();

        firstPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (testMode)
        {
            switch (testState)
            {
                case State.Idle:

                    IdleUpdate();

                    break;

                case State.Jump:

                    JumpUpdate();

                    break;

                case State.Dash:

                    DashUpdate();

                    break;

                case State.HitGround:

                    //print("HitGround");
                    HitGroundUpdate();

                    break;
            }
        }
        else
        {
            CheckState();
        }
        
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Idle:

                IdleUpdate();

                break;

            case State.Jump:

                JumpUpdate();

                break;

            case State.Dash:

                DashUpdate();

                break;

            case State.HitGround:

                //print("HitGround");
                HitGroundUpdate();

                break;
        }
    }

    private void IdleUpdate()
    {
        spriteHandler.ChangeAnim(BossSpriteHandler.Anim.Idle);

        if (currentIdleTime <= 0)
        {
            print("Idle");

            int randMove = UnityEngine.Random.Range(1, Enum.GetValues(typeof(State)).Length);
            currentIdleTime = idleTime;

            if(actionSpamNumber[randMove] >= 2)
            {
                randMove = UnityEngine.Random.Range(1, Enum.GetValues(typeof(State)).Length);
            }

            state = (State)randMove;

            for (int i = 0; i < Enum.GetValues(typeof(State)).Length; i++)
            {
                if(i == randMove)
                {
                    actionSpamNumber[randMove]++;
                }
                else
                {
                    actionSpamNumber[i] = 0;
                }
            }
            
            //state = State.HitGround;
        }
        else
        {
            currentIdleTime -= Time.fixedDeltaTime;
        }
    }

    private void JumpUpdate()
    {
        if (!startState)
        {
            print("Jump");
            spriteHandler.ChangeAnim(BossSpriteHandler.Anim.JumpRight);
            startState = true;
        }
        if (!stopSetValue)
        {
            currentActionTime = jumpTime;
            stopSetValue = true;
        }

        if(currentActionTime <= 0)
        {
            BackToIdle();
        }
        else
        {
            currentActionTime -= Time.fixedDeltaTime;

            bool touchGround = Physics2D.Raycast(feetPos.position, Vector2.down, groundCheckDistance, layerMask);
            bool touchWall = Physics2D.Raycast(transform.position, direction, wallCheckDistance, layerMask);

            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            if (touchGround)
            {
                rb.velocity = Vector2.up * jumpForce;
                spriteHandler.ChangeAnim(BossSpriteHandler.Anim.Land);
            }

            if (touchWall)
            {
                FindAnyObjectByType<AudioManager>().Play("BossAttack");
                direction.x *= -1;
                FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
                spriteHandler.gameObject.transform.localScale = new Vector2(-direction.x, 1);
            }
        }
        
    }
    private void DashUpdate()
    {
        spriteHandler.ChangeAnim(BossSpriteHandler.Anim.Run);

        if (!stopSetValue)
        {
            currentActionTime = jumpTime;
            stopSetValue = true;
        }

        if (currentActionTime <= 0)
        {
            BackToIdle();
        }
        else
        {
            currentActionTime -= Time.fixedDeltaTime;

            bool touchWall = Physics2D.Raycast(transform.position, direction, wallCheckDistance, layerMask);

            rb.velocity = new Vector2(direction.x * dashForce, rb.velocity.y);

            if (touchWall)
            {
                FindAnyObjectByType<AudioManager>().Play("BossAttack");
                direction.x *= -1;
                FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
                spriteHandler.gameObject.transform.localScale = new Vector2(-direction.x,1);
            }
        }
    }
    private void HitGroundUpdate()
    {
        if (!stopSetValue)
        {
            currentActionTime = jumpTime;
            stopSetValue = true;
        }

        if (currentActionTime <= 0)
        {
            BackToIdle();
        }
        else
        {
            currentActionTime -= Time.fixedDeltaTime;

            bool touchGround = Physics2D.Raycast(feetPos.position, Vector2.down, groundCheckDistance, layerMask);

            if (touchGround)
            {
                if (startProjectileMachine)
                {
                    FindAnyObjectByType<AudioManager>().Play("BossAttack");

                    if (!projectileMachines[0].activeInHierarchy && !projectileMachines[1].activeInHierarchy)
                    {
                        projectileMachines[0].SetActive(true);
                    }
                    else if (projectileMachines[0].activeInHierarchy)
                    {
                        projectileMachines[0].SetActive(false);
                        projectileMachines[1].SetActive(true);
                    }
                    else
                    {
                        projectileMachines[1].SetActive(false);
                        projectileMachines[0].SetActive(true);
                    }

                    FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
                    StopCoroutine(CanHitGround());
                    StartCoroutine(CanHitGround());
                    startProjectileMachine = false;
                }
                else
                {
                    if (!stopHitGround)
                    {
                        rb.velocity = Vector2.up * hitGroundJumpForce;
                        startProjectileMachine = true;
                    }
                }
            }
        }
    }

    private void WaitTime()
    {
        
    }

    private IEnumerator CanHitGround()
    {
        stopHitGround = true;
        yield return new WaitForSeconds(hitGroundWaitTime);
        stopHitGround = false;
    }

    private void BackToIdle()
    {
        startState = false;
        rb.velocity = Vector2.zero;
        currentIdleTime = idleTime;
        state = State.Idle;
        stopSetValue = false;
        startProjectileMachine = false;
        stopHitGround = false;
        spriteHandler.gameObject.transform.localScale = new Vector2(-direction.x, 1);
    }

    public override void TakeDamage(int amount)
    {
        if (state == State.None)
            return;

        base.TakeDamage(amount);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        state = State.None;
        rb.velocity = Vector2.zero;
        transform.Find("HitBox").gameObject.SetActive(false);
        flashImg.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        transform.localScale = new Vector3(-1, 1, 1);
        transform.position = firstPos;
        spriteHandler.ChangeAnim(BossSpriteHandler.Anim.Death);

        yield return new WaitForSeconds(0.2f);
        transform.localScale = new Vector3(-1, 1, 1);
        flashImg.SetActive(false);
    }
}
