using UnityEngine;

public class FireRing : MonoBehaviour
{
    [SerializeField] float increaseSizeAmount;
    float currentSize;

    [HideInInspector] public float desTime;

    private void Start()
    {
        Destroy(gameObject,desTime);
    }

    void FixedUpdate()
    {
        currentSize += increaseSizeAmount;

        transform.localScale = new Vector3(currentSize,currentSize,currentSize);
    }
}
