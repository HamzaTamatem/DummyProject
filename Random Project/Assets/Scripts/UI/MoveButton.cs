using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public static event Action<Vector2> OnMovePressed;

    [SerializeField] private MoveDirection moveDirection;
    [SerializeField] private bool isBeingHeld;

    public static bool xMovementButtonHeld = false;
    // public static bool horizontalMovementReversed;

    public static Vector2 MoveInput => moveInput;
    private static Vector2 moveInput = Vector2.zero;
    
    public static event Action OnMoveDown;
    
    private void Start()
    {
        if (moveDirection == MoveDirection.Down)
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                OnMoveDown?.Invoke();
            });
        }

        isBeingHeld = false;
    }

    private void FixedUpdate()
    {
        if (isBeingHeld && (moveDirection == MoveDirection.Right || moveDirection == MoveDirection.Left))
        {
            OnMovePressed?.Invoke(moveInput);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(nameof(OnPointerDown));
        isBeingHeld = true;
        switch (moveDirection)
        {
            case MoveDirection.Right:
                xMovementButtonHeld = true;
                moveInput.x = 1;
                break;
            case MoveDirection.Left:
                xMovementButtonHeld = true;
                moveInput.x = -1;
                break;
            case MoveDirection.Up:
                moveInput.y = 1;
                break;
            case MoveDirection.Down:
                moveInput.y = -1;
                break;
            default:
                break;
        }
        OnMovePressed?.Invoke(moveInput);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(nameof(OnPointerUp));
        isBeingHeld = false;
        switch (moveDirection)
        {
            case MoveDirection.Right:
                xMovementButtonHeld = false;
                moveInput.x = 0;
                break;
            case MoveDirection.Left:
                xMovementButtonHeld = false;
                moveInput.x = 0;
                break;
            case MoveDirection.Up:
                moveInput.y = 0;
                break;
            case MoveDirection.Down:
                moveInput.y = 0;
                break;
            default:
                break;
        }
        OnMovePressed?.Invoke(moveInput);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(nameof(OnPointerEnter));
        switch (moveDirection)
        {
            case MoveDirection.Right:
                moveInput.x = 1;
                break;
            case MoveDirection.Left:
                moveInput.x = -1;
                break;
            case MoveDirection.Up:
                moveInput.y = 1;
                break;
            case MoveDirection.Down:
                moveInput.y = -1;
                break;
            default:
                break;
        }
        OnMovePressed?.Invoke(moveInput);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log(nameof(OnPointerExit));
        switch (moveDirection)
        {
            case MoveDirection.Right:
                moveInput.x = 0;
                break;
            case MoveDirection.Left:
                moveInput.x = 0;
                break;
            case MoveDirection.Up:
                moveInput.y = 0;
                break;
            case MoveDirection.Down:
                moveInput.y = 0;
                break;
            default:
                break;
        }
        OnMovePressed?.Invoke(moveInput);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // Debug.Log(nameof(OnPointerMove));
    }
}

public enum MoveDirection
{
    Right,
    Left,
    Up, 
    Down
}