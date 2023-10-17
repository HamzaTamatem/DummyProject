using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position = transform.GetChild(0).transform.position;
    }
}
