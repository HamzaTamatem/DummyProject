using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static event Action OnGameStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        OnGameStarted?.Invoke();
    }
}
