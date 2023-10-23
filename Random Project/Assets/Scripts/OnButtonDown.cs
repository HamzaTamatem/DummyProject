using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnButtonDown : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent buttonDown;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("-- Button Down Event Detected --");
        buttonDown?.Invoke();
    }
}
