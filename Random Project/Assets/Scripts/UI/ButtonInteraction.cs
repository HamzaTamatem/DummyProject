using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private UnityEvent buttonDown;
    [SerializeField] private UnityEvent buttonUp;

    public bool buttonIsHeld = false;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerDown)} Event Detected from {gameObject.name} --");
        buttonDown?.Invoke();

        buttonIsHeld = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerUp)} Event Detected from {gameObject.name} --");
        buttonUp?.Invoke();
        
        buttonIsHeld = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerEnter)} Event Detected from {gameObject.name} --");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerExit)} Event Detected from {gameObject.name} --");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerMove)} Event Detected from {gameObject.name} --");
    }
}
