using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GiveDamage : MonoBehaviour
{
    [SerializeField] int damage;
    
    public Tilemap tilemap;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (gameObject.TryGetComponent(out Tilemap tilemap) && col.CompareTag("Player"))
        {
            Vector3 tilePosition = TileManager.GetClosestTile(col.transform.position);
            col.GetComponent<PlayerMovement>().PushPlayerAwayFrom(tilePosition, 15f);
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        } else if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerMovement>().PushPlayerAwayFrom(transform.position, 15f);
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (gameObject.TryGetComponent(out Tilemap tilemap) && other.CompareTag("Player"))
    //     {
    //         Vector3 tilePosition = TileManager.GetClosestTile(other.transform.position);
    //         other.GetComponent<PlayerMovement>().PushPlayerAwayFrom(tilePosition, 10f);
    //     } else if (other.CompareTag("Player"))
    //     {
    //         other.GetComponent<PlayerMovement>().PushPlayerAwayFrom(transform.position, 10f);
    //     }
    // }
}
