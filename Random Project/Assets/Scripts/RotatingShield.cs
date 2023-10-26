using UnityEngine;

public class RotatingShield : MonoBehaviour
{
    [SerializeField] float rotatingSpeed;

    void FixedUpdate()
    {
        if (transform.parent.localScale.x < 0)
        {
            transform.Rotate(0, 0, -rotatingSpeed);
        }
        else
        {
            transform.Rotate(0, 0, rotatingSpeed);
        }
    }
}
