using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFader : MonoBehaviour
{
    public ButtonInteraction buttonInteraction;
    
    private Button targetButton;
    private Color normalColor;
    private Color pressedColor;
    private Color tintedColor;

    private void Awake()
    {
        targetButton = GetComponent<Button>();
        normalColor = targetButton.image.color;
        tintedColor = targetButton.colors.pressedColor * normalColor;
    }

    private void OnEnable()
    {
        buttonInteraction.OnButtonDown += FadePressed;
        buttonInteraction.OnButtonUp += FadeNormal;
    }

    private void OnDisable()
    {
        buttonInteraction.OnButtonDown -= FadePressed;
        buttonInteraction.OnButtonUp -= FadeNormal;
    }

    public void FadePressed()
    {
        targetButton.image.DOColor(tintedColor, 0.5f);
    }

    public void FadeNormal()
    {
        targetButton.image.DOColor(normalColor, 0.5f);
    }
}
