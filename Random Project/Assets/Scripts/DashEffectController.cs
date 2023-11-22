using UnityEngine;

public class DashEffectController : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayDashAnimation()
    {
        EnableSpriteRenderer(true);
        animator.Play("DashEffect");
    }

    public void EnableSpriteRenderer(bool b)
    {
        spriteRenderer.enabled = b;
    }

    public void OnAnimationDoneDestroy()
    {
        Destroy(gameObject);
    }
}
