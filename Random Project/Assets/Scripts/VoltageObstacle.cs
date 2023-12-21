using UnityEngine;

public class VoltageObstacle : MonoBehaviour
{
    Collider2D col => GetComponent<Collider2D>();
    Animator anim => GetComponent<Animator>();

    [SerializeField] float startWaitTime;
    float waitTime;

    bool active;

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
}
