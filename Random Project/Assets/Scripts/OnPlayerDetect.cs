using UnityEngine;
using UnityEngine.Events;

public class OnPlayerDetect : MonoBehaviour
{

    public UnityEvent OnDetect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnDetect?.Invoke();
        }
    }
}
