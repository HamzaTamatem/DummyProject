using System;
using DG.Tweening;
using UnityEngine;

public class FadeOutOnPlayerDeath : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += FadeOut;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= FadeOut;
    }

    private void FadeOut()
    {
        canvasGroup.DOFade(0, fadeOutTime);
    }
}
