using System.Collections;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    //Refs
    Transform playerPos;

    //Attributes
    [SerializeField] float health,speed,damage;
    bool stopDmg;

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, playerPos.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
    }

    public void GetHit(float dmg)
    {
        print("Reduce Health");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (stopDmg)
                return;

            print("Get Hit");
            StartCoroutine(CanDmg());
            //col.GetHit(damage);
        }
    }

    private IEnumerator CanDmg()
    {
        stopDmg = true;
        yield return new WaitForSeconds(0.5f);
        stopDmg = false;
    }
}
