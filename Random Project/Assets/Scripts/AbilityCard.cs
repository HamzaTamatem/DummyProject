using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour
{
    [SerializeField] private TMP_Text abilityTitle;
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private Image abilityImage;
    [SerializeField] private AbilityUpgradeCardSO upgradeCardSo;
    [SerializeField] private Button cardButton;
    [SerializeField] private TMP_Text abilityLevel;

    public static event Action<int> OnCardUpgrade;

    public void UpdateCard(AbilityUpgradeCardSO cardSo)
    {
        upgradeCardSo = cardSo;
        abilityTitle.text = upgradeCardSo.abilityTitle;
        abilityDescription.text = upgradeCardSo.abilityDescription;
        abilityImage.sprite = upgradeCardSo.abilitySprite;
        abilityLevel.text = (upgradeCardSo.level+1).ToString();
        // cardButton.onClick.AddListener(upgradeCardSo.IncreaseFireRate);
    }

    public void WipeOut()
    {
        upgradeCardSo = null;
        abilityTitle.text = null;
        abilityDescription.text = null;
        abilityImage.sprite = null;
    }

    public void OnCardClicked()
    {
        upgradeCardSo.IncreaseFireRate();
        OnCardUpgrade?.Invoke(upgradeCardSo.upgradeId);
        upgradeCardSo.level++;
    }
}