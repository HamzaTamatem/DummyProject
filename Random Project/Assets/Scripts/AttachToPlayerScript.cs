using UnityEngine;

public class AttachToPlayerScript : MonoBehaviour
{
    void Awake()
    {
        FindObjectOfType<LifeBattery>().batteryUIFolder = gameObject;
    }
}
