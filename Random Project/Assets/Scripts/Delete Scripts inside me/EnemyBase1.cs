using UnityEngine;

public class EnemyBase1 : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;
    [SerializeField] LayerMask layerMask;
     
    private void FixedUpdate()
    {
        rb.velocity = dir * speed;

        bool hit = Physics2D.Raycast(transform.GetChild(0).transform.position, dir, 2,layerMask);

        if (hit)
        {
            dir = -dir;
            transform.GetChild(0).transform.localScale = new Vector3(-transform.GetChild(0).transform.localScale.x, transform.GetChild(0).transform.localScale.y);
        }
    }

    public void TakeHit(float dmg)
    {
        health -= dmg;

        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
