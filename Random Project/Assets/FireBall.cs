using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    //Attributes
    [SerializeField] float speed;
    [SerializeField] float damage;

    GameObject nearestEnemy;

    private void Awake()
    {
        GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));

        if(nearestEnemy != null)
        {
            Vector3 difference = nearestEnemy.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ + -90);
        }
        
        Destroy(gameObject, 10);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        nearestEnemy = tMin;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy_Base>().GetHit(damage);
            Destroy(gameObject);
        }
    }
}
