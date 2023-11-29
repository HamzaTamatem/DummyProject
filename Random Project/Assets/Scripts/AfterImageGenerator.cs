using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageGenerator : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRendererToCopy;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private Color afterImageColor;
    [SerializeField] private Material afterImageMaterial;

    [Header("Fade")] 
    [SerializeField] private float fadeDuration;
    [SerializeField] [Range(0,1)] private float startingOpacity;

    private Coroutine spawnCoroutine;
    private List<SpriteRenderer> afterImages;

    public void Play()
    {
        Debug.Log(nameof(Play));
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        spawnCoroutine = StartCoroutine(Spawn());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);
            InstantiateAfterImage();
        }
    }

    private void InstantiateAfterImage()
    {
        GameObject go = new GameObject("AfterImage");
        // go.transform.SetParent(this.transform);
        
        go.transform.position = spriteRendererToCopy.transform.position;
        go.transform.rotation = spriteRendererToCopy.transform.rotation;
        go.layer = spriteRendererToCopy.gameObject.layer;

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = spriteRendererToCopy.sprite;
        // sr.material = spriteRendererToCopy.material;
        sr.material = afterImageMaterial;
        sr.sortingLayerID = spriteRendererToCopy.sortingLayerID;
        sr.sortingLayerName = spriteRendererToCopy.sortingLayerName;
        sr.sortingOrder = spriteRendererToCopy.sortingOrder - 1;
        sr.flipX = spriteRendererToCopy.flipX;
        sr.flipY = spriteRendererToCopy.flipY;
        // Set initial opacity
        // sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, startingOpacity);
        // sr.color = new Color(0, 0, 0, startingOpacity);
        sr.color = afterImageColor;

        AfterImage afterImage = go.AddComponent<AfterImage>();
        afterImage.FadeOut(fadeDuration);

        Flashable flashable = go.AddComponent<Flashable>();
        flashable.SetFlashColor(GetComponent<Flashable>().GetFlashColor());
        flashable.SetFlashTime(fadeDuration);
        flashable.CallDamageFlash();
    }
}
