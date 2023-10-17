using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnPoses;

    [SerializeField] float maxSpawnTime;
    float spawnTime;

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

        int randEnemy = Random.Range(0,enemies.Length);
        int randPos = Random.Range(0, spawnPoses.Length);

        Instantiate(enemies[randEnemy], spawnPoses[randPos].position, Quaternion.identity);
    }
}
