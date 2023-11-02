using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour
{

    [Header("Enemy Spawn")]
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnPoses;
    [SerializeField] float[] enemyPersentage;

    [Header("Spawn Numbers")]
    [SerializeField] float maxSpawnTime;
    [SerializeField] float endTimer;
    float spawnTime;

    [SerializeField] int spawnCap;
    [SerializeField] int maxEnemyNumber;
    [SerializeField] int enemiesNumber;

    enum EndMethod { timer, maxNumber };
    [SerializeField] EndMethod endMethod;
    bool endRoom;

    enum Prototype { prototype_1, prototype_2 };
    [SerializeField] Prototype prototype;

    private void Awake()
    {
        spawnTime = maxSpawnTime;
    }

    private void OnEnable()
    {
        XpManager.OnPlayerLevelUp += LevelUp;
    }

    private void Update()
    {
        if (endRoom)
            return;

        SpawnTimer();

        if(endMethod == EndMethod.timer)
        {
            if (endTimer <= 0)
            {
                EndRoom();
            }
            else
            {
                endTimer -= Time.deltaTime;
            }
        }
        
    }

    private void OnDisable()
    {
        XpManager.OnPlayerLevelUp -= LevelUp;
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
            float randEnemy = Random.Range(0, 99);
            int randPos = Random.Range(0, spawnPoses.Length);

            if(randEnemy <= enemyPersentage[0])
            {
                Instantiate(enemies[0], spawnPoses[randPos].position, Quaternion.identity);
                UpdateEnemyNumber(true);
            }
            else if(randEnemy <= enemyPersentage[1])
            {
                Instantiate(enemies[1], spawnPoses[randPos].position, Quaternion.identity);
                UpdateEnemyNumber(true);
            }
            else if (randEnemy <= enemyPersentage[2])
            {
                Instantiate(enemies[2], spawnPoses[randPos].position, Quaternion.identity);
                UpdateEnemyNumber(true);
            }
            else if (randEnemy <= enemyPersentage[3])
            {
                Instantiate(enemies[3], spawnPoses[randPos].position, Quaternion.identity);
                UpdateEnemyNumber(true);
            }

            if(endMethod == EndMethod.maxNumber)
            {
                maxEnemyNumber--;
                if(maxEnemyNumber <= 0)
                {
                    endRoom = true;
                }
            }
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

            if(enemiesNumber <= 0 && maxEnemyNumber <= 0)
            {
                if(prototype == Prototype.prototype_1)
                {
                    FindObjectOfType<RoomManager>().NextRoom();
                }
                else
                {
                    FindObjectOfType<ChallangeRoomManager>().OpenGate();
                }
                
                print("End room");
            }
        }
    }

    private void EndRoom()
    {
        print("End room");
        endRoom = true;
        //FindObjectOfType<RoomManager>().NextRoom();

        if (prototype == Prototype.prototype_1)
        {
            FindObjectOfType<RoomManager>().NextRoom();
        }
        else
        {
            FindObjectOfType<ChallangeRoomManager>().OpenGate();
        }
    }

    private void LevelUp()
    {
        maxSpawnTime -= 0.1f;
    }
}
