using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    [SerializeField] GameObject trapFolder;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            trapFolder.SetActive(true);
        }
    }
}
