using System.Collections;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    public Collectible.XpLevel _xpLevel = Collectible.XpLevel.One;
    
    //Refs
    Transform playerPos;
    [SerializeField] GameObject deathPar;

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
        //print("Reduce Health");
        health -= dmg;

        if (health <= 0)
        {
            GameObject.FindObjectOfType<Enemy_Spawner>().UpdateEnemyNumber(false);
            ScrapSpawner.NewEnemyDied(_xpLevel, transform.position);
            Instantiate(deathPar, transform.position, Quaternion.identity);
            FindObjectOfType<CinemaShake>().Shake(2f, 0.2f);
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
            
            // deal damage to the player
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private IEnumerator CanDmg()
    {
        stopDmg = true;
        yield return new WaitForSeconds(0.5f);
        stopDmg = false;
    }
}
