using UnityEngine;

public class FallTrapDetector : MonoBehaviour
{
    [SerializeField] FallTrap myFallTrap;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            myFallTrap.enabled = true;
            Destroy(gameObject);
        }
    }
}
