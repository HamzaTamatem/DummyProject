using UnityEngine;

public class Collectable : MonoBehaviour
{
    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    Collider2D myCollider => GetComponent<Collider2D>();
    AudioSource mySound => GetComponent<AudioSource>();

    GameObject myParticle;

    private void Awake()
    {
        myParticle = transform.Find("Par").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Hide();
        }
    }

    private void Hide()
    {
        myParticle.SetActive(true);
        mySound.Play();
        sr.enabled = false;
        myCollider.enabled = false;

        Destroy(gameObject,2);
    }
}
