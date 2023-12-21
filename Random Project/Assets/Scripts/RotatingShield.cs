using UnityEngine;
using UnityEngine.Rendering.UI;

public class RotatingShield : MonoBehaviour
{
    [SerializeField] float rotatingSpeed;

    private void Start()
    {
        float randRot = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0f, 0f, randRot);

        int randDirection = Random.Range(0, 2);

        if(randDirection >= 1)
        {
            rotatingSpeed = -rotatingSpeed;
        }
    }

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
