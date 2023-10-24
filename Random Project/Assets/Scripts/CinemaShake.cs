using UnityEngine;
using Cinemachine;

public class CinemaShake : MonoBehaviour
{
    [SerializeField] float shakePower;
    [SerializeField] float time;
    float shakeTime;
    CinemachineVirtualCamera myCam;

    private void Awake()
    {
        myCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if(shakeTime <= 0)
        {
            CinemachineBasicMultiChannelPerlin c = myCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            c.m_AmplitudeGain = 0;
        }
        else
        {
            shakeTime -= Time.deltaTime;
        }
    }

    public void Shake(float power, float timer)
    {
        CinemachineBasicMultiChannelPerlin c = myCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c.m_AmplitudeGain = power;
        shakeTime = timer;
    }
}