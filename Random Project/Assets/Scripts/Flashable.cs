using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flashable : MonoBehaviour
{
    [ColorUsage(true,true)]
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashTime;
    // [SerializeField] private Material material;

    // public Color _originalColor;
    // private SpriteRenderer _spriteRenderer;
    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private Coroutine damageFlashCoroutine;
    
    
    public virtual void Awake()
    {
        // _spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        Init();
        // Debug.Log(_spriteRenderer.gameObject.name);
    }

    public virtual void Start()
    {
        // _originalColor = _spriteRenderer.color;
        // Debug.Log(_originalColor.ToString());
    }

    public void Init()
    {
        materials = new Material[spriteRenderers.Length];
        
        // store materials in all sprite renderers
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    public void CallDamageFlash()
    {
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        // set the color
        SetFlashColor();
        
        // lerp the flash amount
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        
        while (elapsedTime < flashTime)
        {
            // iterate elapsedTime
            elapsedTime += Time.deltaTime;
            
            // lerp the flash amount 
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        // set the color
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_FlashColor", flashColor);
        }
    }

    private void SetFlashAmount(float amount)
    {
        // set the flash amount
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", amount);
        }
    }

    // public IEnumerator FlashCoroutine()
    // {
    //     _spriteRenderer.color = flashColor;
    //     yield return new WaitForSeconds(flashTime);
    //     _spriteRenderer.color = _originalColor;
    //
    // }

    public void Flash()
    {
        // _spriteRenderer.color = _originalColor;
        // StopAllCoroutines();
        // StartCoroutine(FlashCoroutine());
        CallDamageFlash();
    }

    public void SetFlashColor(Color color)
    {
        flashColor = color;
    }

    public Color GetFlashColor()
    {
        return flashColor;
    }

    public void SetFlashTime(float time)
    {
        flashTime = time;
    }
}
