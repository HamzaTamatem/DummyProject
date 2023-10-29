using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BouncyEnemy : Enemy
{
    public enum AttackState
    {
        None,
        Patrol,
        Attack
    }

    [SerializeField] private GameObject particleExlposionPrefab;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private GameObject legs;
    [SerializeField] private float forceMultiplier;
    [SerializeField] private UnityEvent OnDestroy;

    private AttackState _attackState = AttackState.None;
    private Rigidbody2D _rb2d;
    private bool _isTriggered;
    private Vector2 _forceDirection;

    private void Awake()
    {
        _rb2d= GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ChangeState(AttackState.Patrol);
        Random.Range(0, 1);
    }

    public void ChangeState(AttackState newState)
    {
        _attackState = newState;
        switch (_attackState)
        {
            case AttackState.Patrol:
                break;
            case AttackState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }

    public void ChangeState(int intNewState)
    {
        AttackState newState = (AttackState)intNewState;
        Debug.Log($"Changing to state: {newState}");
        ChangeState(newState);
    }

    public void Attack()
    {
        if (_isTriggered)
        {
            return;
        }
        _isTriggered = true;
        Debug.Log(nameof(Attack));
        StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine()
    {
        Debug.Log(nameof(AttackCoroutine));
        
        //remove legs
        legs.transform.DOScale(Vector3.zero, 0.1f);
        legs.transform.DOMoveY(-0.1f, 0.2f);

        // wait certain time
        yield return new WaitForSeconds(0.1f);
        legs.SetActive(false);

        // get random force to apply 
        // forceDirection = new Vector2(Random.Range(0f,1f), Random.Range(0f,1f));
        _forceDirection = (FindObjectOfType<PlayerMovement>().transform.position - transform.localPosition).normalized;
        
        _rb2d.gravityScale = 0;

        // move in opposite direction to prepare player
        transform.DOMove(transform.position - (Vector3)_forceDirection * 1,0.5f);
        yield return new WaitForSeconds(0.1f);
        
        // apply force
        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(_forceDirection * forceMultiplier, ForceMode2D.Impulse);
    }

    public override void GetHit(float amount)
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"OnTriggered with a player. {other.gameObject.name}");
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            other.GetComponent<PlayerMovement>().PausePlayerMovement(0.5f);
        }
        else
        {
            Debug.Log($"OnTriggerred with a non-player. {other.gameObject.name}");
        }
        
        // make particle explosion
        Instantiate(particleExlposionPrefab, transform.position, Quaternion.identity);
        
        // get everything in radius of explosion
        var detectedColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var c in detectedColliders)
        {
            Rigidbody2D r = c.GetComponent<Rigidbody2D>();
            if (r != null)
            {
                if (r.bodyType != RigidbodyType2D.Dynamic)
                {
                    continue;
                }
                Vector2 directionVector = r.transform.position - transform.position;
                r.velocity = Vector2.zero;
                r.AddForce(directionVector * explosionForce, ForceMode2D.Impulse);
                Debug.Log($"Added force to: {c.transform.gameObject.name}");
            }
        }

        Debug.Log($"Destroying {gameObject.name}");
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}