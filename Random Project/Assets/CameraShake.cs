using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //refs
    Transform cam;

    //Attributes
    float power, duration, slowTime;
    bool shouldShake;

    Vector3 startPos;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                cam.localPosition = startPos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowTime;
            }
            else
            {
                cam.localPosition = startPos;
                shouldShake = false;
            }
        }
    }

    public void Shake(float power, float duration, float slowTime)
    {
        if (shouldShake)
            return;

        this.power = power;
        this.duration = duration;
        this.slowTime = slowTime;
        startPos = cam.localPosition;
        shouldShake = true;
    }
}
