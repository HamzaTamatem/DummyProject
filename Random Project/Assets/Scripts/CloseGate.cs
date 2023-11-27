using UnityEngine;

public class CloseGate : MonoBehaviour
{
    bool stopDetect;
    [SerializeField] float shakePower;
    [SerializeField] float shakeTime;

    [SerializeField] GameObject ScreamParticles;
    [SerializeField] Boss_1 boss;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (stopDetect)
                return;

            
            // pause player movement & input
            PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
            // playerMovement.PausePlayerMovement(shakeTime);
            playerMovement.FreezePlayer(shakeTime);
            // playerMovement.PausePlayerInput(shakeTime);
            OpenCloseGates();
            stopDetect = true;
            FindObjectOfType<CinemaShake>().Shake(shakePower, shakeTime);
            Destroy(ScreamParticles, shakeTime);
            //Time.timeScale = 0;
            //FindObjectOfType<PlayerMovement>().PausePlayerMovement(shakeTime);
            Invoke("StartBoss", shakeTime);
        }
    }

    public void OpenCloseGates()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeInHierarchy);
    }

    private void StartBoss()
    {
        //Time.timeScale = 1;
        boss.enabled = true;
    }
}
