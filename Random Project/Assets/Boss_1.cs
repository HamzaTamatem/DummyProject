using System;
using System.Collections;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    enum State { Idle, Jump, Dash, HitGround }
    State state;

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

    float groundCheckDistance;
    float wallCheckDistance;

    bool startProjectileMachine;
    bool stopHitGround;
    bool stopSetValue;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        direction.x = 1;

        groundCheckDistance = 0.1f;
        wallCheckDistance = 2.2f;
    }

    private void FixedUpdate()
    {
        CheckState();
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

                print("HitGround");
                HitGroundUpdate();

                break;
        }
    }

    private void IdleUpdate()
    {
        if (currentIdleTime <= 0)
        {
            int randMove = UnityEngine.Random.Range(1, Enum.GetValues(typeof(State)).Length);
            currentIdleTime = idleTime;
            state = (State)randMove;
            //state = State.HitGround;
        }
        else
        {
            currentIdleTime -= Time.fixedDeltaTime;
        }
    }

    private void JumpUpdate()
    {
        if(!stopSetValue)
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
            }

            if (touchWall)
            {
                direction.x *= -1;
                FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
            }
        }
        
    }
    private void DashUpdate()
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

            bool touchWall = Physics2D.Raycast(transform.position, direction, wallCheckDistance, layerMask);

            rb.velocity = new Vector2(direction.x * dashForce, rb.velocity.y);

            if (touchWall)
            {
                direction.x *= -1;
                FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
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

    private IEnumerator CanHitGround()
    {
        stopHitGround = true;
        yield return new WaitForSeconds(hitGroundWaitTime);
        stopHitGround = false;
    }

    private void BackToIdle()
    {
        currentIdleTime = idleTime;
        state = State.Idle;
        stopSetValue = false;
    }
}