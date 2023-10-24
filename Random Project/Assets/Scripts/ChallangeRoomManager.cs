using UnityEngine;

public class ChallangeRoomManager : MonoBehaviour
{
    [SerializeField] GameObject gateFolder;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            gateFolder.SetActive(true);
        }
    }

    public void OpenGate()
    {
        gateFolder.SetActive(false);
    }
}
