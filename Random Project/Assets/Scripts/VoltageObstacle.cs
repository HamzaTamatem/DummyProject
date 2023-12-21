using System;
using UnityEngine;

public class VoltageObstacle : MonoBehaviour
{
    Collider2D col => GetComponent<Collider2D>();
    Animator anim => GetComponent<Animator>();

    [SerializeField] float startWaitTime;
    [Tooltip("The time it will take for the effect of reversed horizontal movement to take off.")]
    [SerializeField] private float stunTime;
    float waitTime;

    bool active;

    public static event Action<float> OnPlayerHitVoltageBall;

    void Update()
    {
        if(waitTime <= 0)
        {
            Active_DeactiveVoltage();
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void Active_DeactiveVoltage()
    {
        active = !active;

        if(active)
        {
            col.enabled = true;
            anim.Play("Active");
        }
        else
        {
            col.enabled = false;
            anim.Play("Deactive");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerHitVoltageBall?.Invoke(stunTime);
        }
    }
}
