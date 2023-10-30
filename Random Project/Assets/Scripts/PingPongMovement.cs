using UnityEngine;

public class PingPongMovement : MonoBehaviour
{

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed;
    [SerializeField] private float pingPongTime;
    
    void Update()
    {
        pingPongTime += speed * Time.deltaTime;
        float pingPongValue = Mathf.PingPong(pingPongTime, 1.0f);

        // Interpolate the position between the start and end points using pingPongValue.
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, pingPongValue);
    }
}
