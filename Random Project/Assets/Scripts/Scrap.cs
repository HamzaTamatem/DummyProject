using UnityEngine;
using UnityEngine.UI;

public class Scrap : Collectible
{
    public XpLevel xpLevel = XpLevel.One;
    public int xpGain;

    [SerializeField] private SpriteRenderer scrapImage;
    [SerializeField] private Color levelOneColor;
    [SerializeField] private Color levelTwoColor;
    [SerializeField] private Color levelThreeColor;

    private float priorityOnePercentage;
    private float priorityTwoPercentage;
    private float priorityThreePercentage;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: Do something
            XpManager.Instance.AddXp(xpGain);
            
            Destroy(gameObject);
        }
    }

    public void SetUpScrapStats()
    {
        switch (xpLevel)
        {
            case XpLevel.One:
                xpGain = 1;
                scrapImage.color = levelOneColor;
                break;
            case XpLevel.Two:
                xpGain = 2;
                scrapImage.color = levelTwoColor;
                break;
            case XpLevel.Three:
                xpGain = 3;
                scrapImage.color = levelThreeColor;
                break;
            default:
                break;
        }
    }

    public void SetXpLevel(XpLevel _xpLevel)
    {
        xpLevel = _xpLevel;
        SetUpScrapStats();
    }
}
