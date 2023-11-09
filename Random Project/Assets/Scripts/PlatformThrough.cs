using System;
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

    private void OnEnable()
    {
        MoveButton.OnMoveDown += StopCollider;
    }

    private void OnDisable()
    {
        MoveButton.OnMoveDown -= StopCollider;
    }

    private void Update()
    {
        //Change the S key to match the player controls
        if (Input.GetKeyDown(KeyCode.S) && canThrough)
        {
            StopCollider();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canThrough = true;
        }
    }

    private IEnumerator StopColliderCoroutine()
    {
        myCollider.enabled = false;
        canThrough = false;
        yield return new WaitForSeconds(0.5f);
        myCollider.enabled = true;
    }

    public void StopCollider()
    {
        StartCoroutine(StopColliderCoroutine());
    }
}
