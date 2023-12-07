using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFade : MonoBehaviour
{

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void FadeOut()
    {
        image.DOFade(0f, 1);
    }

    public void FadeIn()
    {
        image.DOFade(1f, 1f);
    }
}
