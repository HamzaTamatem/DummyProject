using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Controls _controls;
    
    private float movementX;
    private Vector2 movement;

    private void Awake()
    {
        _controls = new Controls();
        _rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Player.Move.performed -= OnMovementPerformed;
        _controls.Player.Move.canceled -= OnMovementCanceled;
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
        _rb.velocity = new Vector2(movementX * moveSpeed, _rb.velocity.y);
    }

    private void Jump()
    {
        if (_rb.velocity.y == 0)
        {
            _rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
        }
    }
}
