using UnityEngine;

[CreateAssetMenu(fileName = "AbilityUpgradeCardData", menuName = "ScriptableObjects/AbilityUpgradeCard")]
public class AbilityUpgradeCardSO : ScriptableObject
{
    public int upgradeId;
    // public UnityEvent onUpgrade;
    public string abilityTitle;
    public string abilityDescription;
    public int level;
    public Sprite abilitySprite;
    public Ability ability;

    // public void InvokeOnUpgrade()
    // {
    //     onUpgrade?.Invoke();
    //     UpgradeManager.Instance.DisplayNewSetOfUpgrades();
    // }

    public void IncreaseFireRate()
    {
        UpgradeManager.Instance.IncreaseFireRate(ability);
    }
}