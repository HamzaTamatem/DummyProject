using UnityEngine;

public class EnemyBase1 : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] float speed;
    [SerializeField] Vector2 dir;
    [SerializeField] LayerMask layerMask;
     
    private void FixedUpdate()
    {
        rb.velocity = dir * speed;

        bool hit = Physics2D.Raycast(transform.position, dir, 2,layerMask);

        if (hit)
        {
            dir = -dir;
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(-2, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(2, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if(col.gameObject.layer == 6)
    //    {
    //        dir = -dir;
    //        if(dir.x < 0)
    //        {
    //            transform.localScale = new Vector3(-2, transform.localScale.y, transform.localScale.z);
    //        }
    //        else
    //        {
    //            transform.localScale = new Vector3(2, transform.localScale.y, transform.localScale.z);
    //        }
            
    //    }
    //}
}
