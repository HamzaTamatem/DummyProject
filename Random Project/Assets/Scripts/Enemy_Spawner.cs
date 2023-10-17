using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnPoses;
    [SerializeField] float[] enemyPersentage;

    [SerializeField] float maxSpawnTime;
    float spawnTime;
    [SerializeField] int spawnCap;
    int enemiesNumber;

    private void Awake()
    {
        spawnTime = maxSpawnTime;
    }

    private void Update()
    {
        SpawnTimer();
    }

    private void SpawnTimer()
    {
        if (spawnTime <= 0)
        {
            Spawn();
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    private void Spawn()
    {
        spawnTime = maxSpawnTime;

        if(enemiesNumber < spawnCap)
        {
            float randEnemy = Random.Range(0, 100);
            int randPos = Random.Range(0, spawnPoses.Length);

            if(randEnemy <= enemyPersentage[0])
            {
                Instantiate(enemies[0], spawnPoses[randPos].position, Quaternion.identity);
            }
            else if(randEnemy <= enemyPersentage[1])
            {
                Instantiate(enemies[1], spawnPoses[randPos].position, Quaternion.identity);
            }
            else if (randEnemy <= enemyPersentage[2])
            {
                Instantiate(enemies[2], spawnPoses[randPos].position, Quaternion.identity);
            }
            else if (randEnemy <= enemyPersentage[3])
            {
                Instantiate(enemies[3], spawnPoses[randPos].position, Quaternion.identity);
            }

            UpdateEnemyNumber(true);
        }
    }

    public void UpdateEnemyNumber(bool increase)
    {
        if (increase)
        {
            enemiesNumber++;
        }
        else
        {
            enemiesNumber--;
        }
    }
}
