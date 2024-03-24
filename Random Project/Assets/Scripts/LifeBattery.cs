using UnityEngine;

public class LifeBattery : MonoBehaviour
{
    [SerializeField] float startBattery;
    float currentBattery;

    bool stop;

    [HideInInspector] public GameObject batteryUIFolder;

    float persentage;

    GameObject[] batteries;

    private void OnEnable()
    {
        EndLine.ReachEndLine += ReachedEndLine;
    }
    private void OnDisable()
    {
        EndLine.ReachEndLine -= ReachedEndLine;
    }

    private void Awake()
    {
        currentBattery = startBattery;
    }

    private void Start()
    {
        batteries = new GameObject[7];
        for (int i = 0; i < batteryUIFolder.transform.childCount; i++)
        {
            batteries[i] = batteryUIFolder.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (stop)
            return;

        if(currentBattery <= 0)
        {
            //print("Death");
            stop = true;
            GetComponent<PlayerHealth>().TakeDamage(GetComponent<PlayerHealth>().currentHealth);
        }
        else
        {
            currentBattery -= Time.deltaTime;

            //batteryBarFill.fillAmount = currentBattery / startBattery;

            persentage = currentBattery / startBattery;

            CalculateBatteryUI();
        }
    }

    public void AddBattery(float ammountAdded)
    {
        if(ammountAdded + currentBattery > startBattery)
        {
            currentBattery = startBattery;
        }
        else
        {
            currentBattery += ammountAdded;
        }

        //batteryBarFill.fillAmount = currentBattery / startBattery;
    }

    private void CalculateBatteryUI()
    {
        if(persentage >= 0.9f)
        {
            batteries[6].SetActive(true);
            batteries[5].SetActive(true);
            batteries[4].SetActive(true);
            batteries[3].SetActive(true);
            batteries[2].SetActive(true);
            batteries[1].SetActive(true);
            batteries[0].SetActive(true);
        }
        else if(persentage <= 0.9f && persentage > 0.8f)
        {
            batteries[6].SetActive(false);
            batteries[5].SetActive(true);
            batteries[4].SetActive(true);
            batteries[3].SetActive(true);
            batteries[2].SetActive(true);
            batteries[1].SetActive(true);
            batteries[0].SetActive(true);
        }
        else if (persentage <= 0.8f && persentage > 0.7f)
        {
            batteries[5].SetActive(false);
            batteries[4].SetActive(true);
            batteries[3].SetActive(true);
            batteries[2].SetActive(true);
            batteries[1].SetActive(true);
            batteries[0].SetActive(true);
        }
        else if (persentage <= 0.6f && persentage > 0.5f)
        {
            batteries[4].SetActive(false);
            batteries[3].SetActive(true);
            batteries[2].SetActive(true);
            batteries[1].SetActive(true);
            batteries[0].SetActive(true);
        }
        else if (persentage <= 0.5f && persentage > 0.4f)
        {
            batteries[3].SetActive(false);
            batteries[2].SetActive(true);
            batteries[1].SetActive(true);
            batteries[0].SetActive(true);
        }
        else if (persentage <= 0.4f && persentage > 0.3f)
        {
            batteries[2].SetActive(false);
            batteries[1].SetActive(true);
            batteries[0].SetActive(true);
        }
        else if (persentage <= 0.3f && persentage > 0.2f)
        {
            batteries[1].SetActive(false);
            batteries[0].SetActive(true);
        }
        else if (persentage <= 0.1f)
        {
            batteries[0].SetActive(false);
        }
    }

    private void ReachedEndLine()
    {
        stop = true;
    }
}
