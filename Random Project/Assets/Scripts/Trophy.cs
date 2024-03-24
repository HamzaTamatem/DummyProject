using UnityEngine;

public class Trophy : MonoBehaviour
{
    public enum Score { Gold, Silver, Copper }
    Score score;

    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    
    [SerializeField] Sprite[] winSprite;
    [SerializeField] GameObject myParticles;

    public void ChangeSprite(Score newScore)
    {
        sr.sprite = winSprite[(int)newScore];
        myParticles.SetActive(true);
        //sr.sprite = winSprite;
    }
}
