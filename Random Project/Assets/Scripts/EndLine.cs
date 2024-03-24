using System;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public static event Action ReachEndLine;

    [SerializeField] AudioSource[] sounds;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ReachEndLine?.Invoke();

            FindObjectOfType<SpeedRunManager>().EndLine();
            GetComponent<Collider2D>().enabled = false;

            sounds[0].Play();
            sounds[1].Play();
        }
    }
}
