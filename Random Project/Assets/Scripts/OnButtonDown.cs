using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnButtonDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent buttonDown;

    public bool buttonIsHeld = false;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("-- Button Down Event Detected --");
        buttonDown?.Invoke();

        buttonIsHeld = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonIsHeld = false;
    }
}
