using UnityEngine;

public class AxeHang : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.7f; // Adjust the rotation speed as needed
    [SerializeField] float pingPongRange = 85f; // Adjust the range of the ping-pong motion

    private void Update()
    {
        // Calculate the t value for ease-in and ease-out using SmoothStep
        float t = Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time * rotationSpeed, 1f));

        // Map the t value to the ping-pong range
        float easedRotation = Mathf.Lerp(-pingPongRange, pingPongRange, t);

        // Apply the eased rotation to the axe around the Y-axis
        transform.rotation = Quaternion.Euler(0f, 0f, easedRotation);
    }
}
