using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenFade : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] [Range(0,1)] private float startingOpacity;
    [Tooltip("How long it will pause before fading in.")] 
    [SerializeField] private float fadeInPauseDuration;

    private Image imageToFade;
    [SerializeField] private SpriteRenderer beforeLastFade;

    private void Awake()
    {
        imageToFade = GetComponent<Image>();
        
        // make to sure to start at required opacity
        imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, startingOpacity);
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
        beforeLastFade.DOFade(2, fadeTime).OnComplete(() =>
        {
            imageToFade.DOFade(2, fadeTime)
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
        imageToFade.DOFade(0, fadeTime);
    }
    
    
}
