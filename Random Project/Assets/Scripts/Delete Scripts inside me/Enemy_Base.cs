using System;
using System.Collections;
using UnityEngine;

public class Enemy_Base : Flashable
{
    public Collectible.XpLevel _xpLevel = Collectible.XpLevel.One;
    
    //Refs
    Transform playerPos;
    [SerializeField] GameObject deathPar;

    //Attributes
    [SerializeField] private int health;
    [SerializeField] float speed;
    [SerializeField] private int damage;
    
    [SerializeField] private float speedLevelUpRate;
    [SerializeField] private float healthLevelUpRate;
    bool stopDmg;

    public override void Awake()
    {
        base.Awake();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        XpManager.OnPlayerLevelUp += LevelUpSpeed;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, playerPos.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        XpManager.OnPlayerLevelUp -= LevelUpSpeed;
    }

    public void GetHit(int dmg)
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
        else
        {
            Flash();
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

    private void LevelUpSpeed()
    {
        speed += speedLevelUpRate;
    }

    // private void LevelUpHealth()
    // {
    //     health += healthLevelUpRate;
    // }
}
