using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] LayerMask teleportAllowedTo;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == teleportAllowedTo.value)
        {
            col.transform.position = transform.GetChild(0).transform.position;
        }
    }
}
