using TMPro;
using UnityEngine;

public class SpeedRunManager : MonoBehaviour
{
    
    [SerializeField] TMP_Text timerTxt;
    float timer;
    bool stopTimer;

    [SerializeField] float perfectTime;

    [Header("Components Folder")]
    [SerializeField] GameObject batteryFolder;
    [SerializeField] GameObject enemyFolder;
    int batteryAmount;
    int maxBatteryAmount;
    int enemyAmount;
    int maxEnemyAmount;

    [Header("Trophies")]
    [SerializeField] Trophy timerTrophy;
    [SerializeField] Trophy batteryTrophy;
    [SerializeField] Trophy enemyTrophy;

    [SerializeField] GameObject EndGate;

    private void OnEnable()
    {
        EnemyBase.OnEnemyDie += EnemyKilled;
    }
    private void OnDisable()
    {
        EnemyBase.OnEnemyDie -= EnemyKilled;
    }

    private void Awake()
    {
        batteryAmount = batteryFolder.transform.childCount;
        maxBatteryAmount = batteryAmount;

        enemyAmount = enemyFolder.transform.childCount;
        maxEnemyAmount = enemyAmount;

        EndGate.SetActive(false);
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (stopTimer)
            return;

        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        timerTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void AddBattery()
    {
        batteryAmount--;
    }

    public void EnemyKilled()
    {
        enemyAmount--;
    }

    public void EndLine()
    {
        stopTimer = true;
        EndGate.SetActive(true);

        FindObjectOfType<CinemaShake>().Shake(1, 1);

        //Timer Score, if player reach the end of the line with less than 50% of perfect time they will have
        //silver trophy
        if(timer <= perfectTime)
        {
            timerTrophy.ChangeSprite(Trophy.Score.Gold);
        }
        else if (timer > perfectTime + (perfectTime * 50 / 100))
        {
            timerTrophy.ChangeSprite(Trophy.Score.Silver);
        }
        else
        {
            timerTrophy.ChangeSprite(Trophy.Score.Copper);
        }

        //Battery Score, if player collect more than 70% of all batteries they will have
        //silver trophy
        if (batteryAmount <= 0)
        {
            batteryTrophy.ChangeSprite(Trophy.Score.Gold);
        }
        else if(batteryAmount <= maxBatteryAmount - (maxBatteryAmount * 70 / 100))
        {
            batteryTrophy.ChangeSprite(Trophy.Score.Silver);
        }
        else
        {
            batteryTrophy.ChangeSprite(Trophy.Score.Copper);
        }


        //Battery Score, if player kill more than 70% of all enemies they will have
        //silver trophy
        if (enemyAmount <= 0)
        {
            enemyTrophy.ChangeSprite(Trophy.Score.Gold);
        }
        else if (enemyAmount <= maxEnemyAmount - (maxEnemyAmount * 70 / 100))
        {
            enemyTrophy.ChangeSprite(Trophy.Score.Silver);
        }
        else
        {
            enemyTrophy.ChangeSprite(Trophy.Score.Copper);
        }
    }
}
