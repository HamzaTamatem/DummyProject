using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private UnityEvent buttonDown;
    public event Action OnButtonDown;
    
    [SerializeField] private UnityEvent buttonUp;
    public event Action OnButtonUp;


    public bool buttonIsHeld = false;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerDown)} Event Detected from {gameObject.name} --");
        buttonDown?.Invoke();
        OnButtonDown?.Invoke();

        buttonIsHeld = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log($"-- {nameof(OnPointerUp)} Event Detected from {gameObject.name} --");
        buttonUp?.Invoke();
        OnButtonUp?.Invoke();
        
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
