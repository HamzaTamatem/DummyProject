using UnityEngine;

public class CloseGate : MonoBehaviour
{
    bool stopDetect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (stopDetect)
                return;

            OpenCloseGates();
            stopDetect = true;
        }
    }

    public void OpenCloseGates()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
    }
}
