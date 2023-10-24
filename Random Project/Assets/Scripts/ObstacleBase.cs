using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] float speed;
    enum Directions { down, up, right, left }
    [SerializeField] Directions directions;

    Vector2 direction;

    private void Start()
    {
        SetDirections();
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void SetDirections()
    {
        switch (directions)
        {
            case Directions.down:
                direction = Vector2.down;
                break;
            case Directions.up:
                direction = Vector2.up;
                break;
            case Directions.right:
                direction = Vector2.right;
                break;
            case Directions.left:
                direction = Vector2.left;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Hider")
        {
            gameObject.SetActive(false);
        }
    }
}
