using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<AbilityUpgradeCardSO> upgradeCards;
    [SerializeField] private List<AbilityCard> cardsInsidePopup;
    
    [SerializeField] private GameObject levelUpPopup;
    [SerializeField] private ShootingBase playerShootingBase;

    private List<int> currentUpgrades = new List<int>();
    private List<AbilityUpgradeCardSO> currentUpgradesToPickFrom = new List<AbilityUpgradeCardSO>();

    public static UpgradeManager Instance;
    
    private void Awake()
    {
        ResetLevels();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        XpManager.OnPlayerLevelUp += DisplayNewSetOfUpgrades;
        AbilityCard.OnCardUpgrade += AddToCurrentUpgrades;
    }

    private void Start()
    {
        currentUpgradesToPickFrom = new List<AbilityUpgradeCardSO>(upgradeCards);
    }

    private void OnDisable()
    {
        XpManager.OnPlayerLevelUp -= DisplayNewSetOfUpgrades;
        AbilityCard.OnCardUpgrade -= AddToCurrentUpgrades;
    }

    [ContextMenu("DisplayNewSetOfUpgrades()")]
    public void DisplayNewSetOfUpgrades()
    {
        // remove old cards
        for (int i = 0; i < cardsInsidePopup.Count; i++)
        {
            cardsInsidePopup[i].WipeOut();
        }
        
        // make a new list that doesn't have the upgrades that are already owned
        currentUpgradesToPickFrom = new List<AbilityUpgradeCardSO>(upgradeCards);
        
        // remove already owned cards
        // var tempList = new List<AbilityUpgradeCardSO>();
        // foreach (var upgrade in currentUpgradesToPickFrom)
        // {
        //     foreach (var ownedUpgrade in currentUpgrades)
        //     {
        //         if (ownedUpgrade == upgrade.upgradeId)
        //         {
        //             tempList.Add(upgrade);
        //         }
        //     }
        // }

        // foreach (var tempUpgrade in tempList)
        // {
        //     currentUpgradesToPickFrom.Remove(tempUpgrade);
        // }
        
        // display new ones
        int count = 0;

        // for (int i = 0; i < cardsInsidePopup.Count; i++)
        // {
        //     for (int j = 0; j < currentUpgradesToPickFrom.Count; j++)
        //     {
        //         if (!currentUpgrades.Contains(currentUpgradesToPickFrom[j].upgradeId))
        //         {
        //             cardsInsidePopup[i].UpdateCard(currentUpgradesToPickFrom[j]);
        //             currentUpgradesToPickFrom.RemoveAt(j);
        //             break;
        //         }
        //         else
        //         {
        //             Debug.Log($"{currentUpgradesToPickFrom[j].upgradeId} is already added to a card");
        //         }
        //     }
        // }
        
        for (int i = 0; i < cardsInsidePopup.Count; i++)
        {
            cardsInsidePopup[i].UpdateCard(upgradeCards[i]);
        }
        
        levelUpPopup.SetActive(true);
    }

    public void IncreaseFireRate(Ability ability)
    {
        playerShootingBase.IncreaseFireRate((int)ability);
    }

    public void IncreaseLifetime(Ability ability)
    {
        
    }
    
    public void AddToCurrentUpgrades(int upgradeId)
    {
        currentUpgrades.Add(upgradeId);
    }

    public void ResetLevels()
    {
        foreach (var cardSo in upgradeCards)
        {
            cardSo.level = 0;
        }
    }
}


