using System;
using UnityEngine;
using UnityEngine.UI;

public class XpManager : MonoBehaviour
{
    [SerializeField] private Image xpBar;
    [SerializeField] private XpTable xpTable;

    [SerializeField] private GameObject levelUpPopup;
    
    public int currentXp;
    public int requiredXp;
    public int currentLevel = 0;

    public static XpManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    public void AddXp(int amount)
    {
        Debug.Log(nameof(AddXp));
        currentXp += amount;
        if (currentXp > requiredXp)
        {
            // TODO: pause game, display new upgrades
            LevelUp();
        }
        UpdateXpBar();
    }

    public void LevelUp()
    {
        Debug.Log(nameof(LevelUp));
        currentLevel++;
        currentXp = 0;
        requiredXp = GetRequiredXp();
        levelUpPopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Init()
    {
        currentLevel = 0;
        currentXp = 0;
        requiredXp = GetRequiredXp();
        UpdateXpBar();
    }

    public int GetRequiredXp()
    {
        return xpTable.XpList[currentLevel];
    }

    public void UpdateXpBar()
    {
        xpBar.fillAmount = (float)currentXp / requiredXp;
    }
}
