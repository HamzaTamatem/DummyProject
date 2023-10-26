using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformThrough : MonoBehaviour
{
    bool canThrough;
    TilemapCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<TilemapCollider2D>();
    }

    private void Update()
    {
        //Change the S key to match the player controls
        if (Input.GetKeyDown(KeyCode.S) && canThrough)
        {
            StartCoroutine(StopCollider());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canThrough = true;
        }
    }

    IEnumerator StopCollider()
    {
        myCollider.enabled = false;
        canThrough = false;
        yield return new WaitForSeconds(0.5f);
        myCollider.enabled = true;
    }
}
