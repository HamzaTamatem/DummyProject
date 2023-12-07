using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Collectible : MonoBehaviour
{
    public abstract void OnTriggerEnter2D(Collider2D other);
    
    public enum XpLevel
    {
        One,
        Two,
        Three
    }
}
