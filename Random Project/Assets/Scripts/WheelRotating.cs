using UnityEngine;

public class WheelRotating : MonoBehaviour
{
    [SerializeField] float rotatingSpeed;

    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotatingSpeed);
    }
}
