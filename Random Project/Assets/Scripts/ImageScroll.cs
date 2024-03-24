using UnityEngine;
using UnityEngine.UI;

public class ImageScroll : MonoBehaviour
{
    [SerializeField] RawImage background;
    [SerializeField] Vector2 pos;

    void Update()
    {
        background.uvRect = new Rect(background.uvRect.position + pos * Time.deltaTime, background.uvRect.size);
    }
}
