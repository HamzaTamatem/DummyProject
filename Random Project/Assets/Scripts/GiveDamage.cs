using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GiveDamage : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] private PlayerMovement.PushDirection pushDirection = PlayerMovement.PushDirection.None;

    private bool recentlyGaveDamage = false;
    
    public Tilemap tilemap;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (recentlyGaveDamage)
        {
            return;
        }
        
        if (gameObject.TryGetComponent(out Tilemap tilemap) && col.CompareTag("Player"))
        {
            //Vector3 tilePosition = TileManager.GetClosestTile(col.transform.position);
            //col.GetComponent<PlayerMovement>().PushPlayerAwayFrom(tilePosition, 15f);
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
            FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
        } else if (col.CompareTag("Player"))
        {
            FindObjectOfType<CinemaShake>().Shake(5, 0.5f);
            PauseGivingDamage(0.5f);
            //if (pushDirection is PlayerMovement.PushDirection.Custom or PlayerMovement.PushDirection.None)
            //{
                
            //    col.GetComponent<PlayerMovement>().PushPlayerAwayFrom(transform.position, 15f);
            //}
            //else
            //{
            //    col.GetComponent<PlayerMovement>().PushPlayerInDirection(pushDirection, 15f);
            //}
            col.GetComponent<PlayerHealth>().TakeDamage(damage);

            
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (recentlyGaveDamage)
        {
            return;
        }
        
        if (gameObject.TryGetComponent(out Tilemap tilemap) && col.CompareTag("Player"))
        {
            //Vector3 tilePosition = TileManager.GetClosestTile(col.transform.position);
            //col.GetComponent<PlayerMovement>().PushPlayerAwayFrom(tilePosition, 15f);
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        } else if (col.CompareTag("Player"))
        {
            PauseGivingDamage(0.5f);
            //if (pushDirection is PlayerMovement.PushDirection.Custom or PlayerMovement.PushDirection.None)
            //{
                
            //    col.GetComponent<PlayerMovement>().PushPlayerAwayFrom(transform.position, 15f);
            //}
            //else
            //{
            //    col.GetComponent<PlayerMovement>().PushPlayerInDirection(pushDirection, 15f);
            //}
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

    private IEnumerator PauseGivingDamageCoroutine(float duration)
    {
        recentlyGaveDamage = true;
        yield return new WaitForSeconds(duration);
        recentlyGaveDamage = false;
    }

    public void PauseGivingDamage(float duration)
    {
        StartCoroutine(PauseGivingDamageCoroutine(duration));
    }

    private void OnDisable()
    {
        recentlyGaveDamage = false;
    }
}
