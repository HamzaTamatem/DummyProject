using UnityEngine;

public class AnimEdetor : MonoBehaviour
{
    SpriteRenderer sr => GetComponent<SpriteRenderer>();

    [SerializeField] float changeSpriteTime;
    [SerializeField] Sprite[] sprites;

    private void Start()
    {
        InvokeRepeating("ChangeSprite", changeSpriteTime, changeSpriteTime);
    }

    private void ChangeSprite()
    {
        if(sr.sprite == sprites[0])
        {
            sr.sprite = sprites[1];
        }
        else
        {
            sr.sprite = sprites[0];
        }
    }
}
