using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public enum PlayerAbility
    {
        Fireball
    }
    
    public static AbilityManager Instance;

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

    private void LevelUpAbility(PlayerAbility playerAbility)
    {
        // level up the ability
        
        // unpause the game
        PauseGame(false);
    }

    public void PauseGame(bool value)
    {
        Time.timeScale = value ? 0f : 1f;
    }
}
