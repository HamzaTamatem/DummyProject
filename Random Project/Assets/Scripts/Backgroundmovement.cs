using UnityEngine;

public class Backgroundmovement : MonoBehaviour
{
    Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothFactor;

    void Start()
    {
        target = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            target = Camera.main.transform;
        }

        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = new Vector3(transform.position.x , target.transform.position.y , transform.position.z);
        Vector3 smoothPotion = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = targetPosition - offset;
    }
}
