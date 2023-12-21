using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] GameObject lazer;

    [SerializeField] float startWaitTime;
    float waitTime;

    bool lazerIsOn;

    private void Awake()
    {
        waitTime = startWaitTime;
    }

    private void Update()
    {
        if(waitTime <= 0)
        {
            Active_DeactiveLazer();
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void Active_DeactiveLazer()
    {
        lazerIsOn = !lazerIsOn;

        lazer.SetActive(lazerIsOn);
    }
}
