using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] LayerMask allowedLayers;

    private void Start()
    {
        Debug.Log($"The layers allowed are: {allowedLayers} | {allowedLayers.value}");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the collider's layer is included in the allowedLayers mask.
        if ((allowedLayers.value & (1 << col.gameObject.layer)) != 0)
        {
            // The collider belongs to one of the allowed layers.
            // Handle the logic for when an allowed collider enters the trigger.
            Debug.Log("Collider on an allowed layer entered the trigger!");
            col.transform.position = transform.GetChild(0).transform.position;
        }
        else
        {
            // The collider does not belong to an allowed layer.
            // Handle the logic for when a disallowed collider enters the trigger.
            Debug.Log("Collider on a disallowed layer entered the trigger!");
        }
        // if(1 << col.gameObject.layer == allowedLayers.value)
        // {
        //     col.transform.position = transform.GetChild(0).transform.position;
        // }
    }
}
