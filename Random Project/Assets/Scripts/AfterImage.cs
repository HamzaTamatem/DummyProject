using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //Debug.Log("Started with opacity: " + sr.color.a);
    }

    public void FadeOut(float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(sr.color.a, 0f, elapsedTime / fadeDuration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the sprite is completely faded out
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        
        // Destroy the after image after it is completely faded out
        Destroy(gameObject);
    }
}
