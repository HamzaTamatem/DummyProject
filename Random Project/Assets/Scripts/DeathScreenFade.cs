using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DeathScreenFade : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] [Range(0,1)] private float startingOpacity;
    [Tooltip("How long it will pause before fading in.")] 
    [SerializeField] private float fadeInPauseDuration;

    private SpriteRenderer spriteToFade;
    [SerializeField] private SpriteRenderer secondSpriteFade;

    private void Awake()
    {
        spriteToFade = GetComponent<SpriteRenderer>();
        
        // make to sure to start at required opacity
        spriteToFade.color = new Color(spriteToFade.color.r, spriteToFade.color.g, spriteToFade.color.b, startingOpacity);
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += FadeOut;
        PlayerHealth.OnPlayerDeath += FadeIn;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= FadeOut;
        PlayerHealth.OnPlayerDeath -= FadeIn;
    }

    [ContextMenu("FadeIn()")]
    public IEnumerator FadeInCoroutine(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        spriteToFade.DOFade(1, fadeTime).OnComplete(() =>
        {
            secondSpriteFade.DOFade(1, fadeTime)
                .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        });
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine(fadeInPauseDuration));
    }

    [ContextMenu("FadeOut()")]
    public void FadeOut()
    {
        spriteToFade.DOFade(0, fadeTime);
    }
    
    
}
