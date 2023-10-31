using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartParent;

    private List<Image> _heartImages = new List<Image>();
    private int _numberOfHearts;

    public static event Action<int> OnNumberOfHeartsChanged;
    public static HealthManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OnNumberOfHeartsChanged += UpdateHearts;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        UpdateHearts(_numberOfHearts);
    }

    private void OnDisable()
    {
        OnNumberOfHeartsChanged -= UpdateHearts;
    }

    public void SetNumberOfHearts(int amount)
    {
        _numberOfHearts = amount;
    }
    
    void Init()
    {
        for (int i = 0; i < _numberOfHearts; i++)
        {
            AddCompleteHeart();
        }
    }

    public void AddCompleteHeart()
    {
        GameObject newImage = Instantiate(heartPrefab, heartParent);
        if (newImage.transform.GetChild(0).TryGetComponent(out Image image))
        {
            _heartImages.Add(image);
        }
    }

    public void UpdateHearts(int amount)
    {
        _numberOfHearts = amount;
        foreach (var img in _heartImages)
        {
            img.enabled = false;
        }

        for (int i = 0; i < amount; i++)
        {
            _heartImages[i].enabled = true;
        }
    }

    public static void UpdateNumberOfHearts(int amount)
    {
        OnNumberOfHeartsChanged?.Invoke(amount);
    }
}
