using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody2D.AddForce(direction * speed, ForceMode2D.Impulse);
        
        // Calculate the angle in radians
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle to degrees
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        
        // rotate the projectile to be facing the direction
        transform.rotation = Quaternion.Euler(0,0,angleInDegrees-90);
    }
}
