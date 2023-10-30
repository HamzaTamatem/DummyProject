using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flashable : MonoBehaviour
{
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashDuration;

    public Color _originalColor;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // Debug.Log(_spriteRenderer.gameObject.name);
    }

    private void Start()
    {
        _originalColor = _spriteRenderer.color;
        // Debug.Log(_originalColor.ToString());
    }

    public IEnumerator FlashCoroutine()
    {
        _spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.color = _originalColor;

    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }
}
