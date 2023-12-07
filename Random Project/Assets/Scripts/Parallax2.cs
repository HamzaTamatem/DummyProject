using UnityEngine;

public class Parallax2 : MonoBehaviour
{

    [SerializeField] Vector2 parallaxSpeed;

    private Transform cam;

    float startPos, length;

    private void Start()
    {
        cam = Camera.main.transform;

        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxSpeed.x));
        float distance = (cam.transform.position.x * parallaxSpeed.x);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(temp > startPos + length)
        {
            startPos += length;
        }
        else if(temp < startPos - length)
        {
            startPos -= length;
        }
    }

}
