using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartParent;

    private List<Image> heartImages = new List<Image>();
    private int numberOfHearts;

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
        // Init();
        // UpdateHearts(numberOfHearts);
    }

    private void OnDisable()
    {
        OnNumberOfHeartsChanged -= UpdateHearts;
    }

    public void SetNumberOfHearts(int amount)
    {
        numberOfHearts = amount;
    }
    
    public void Init()
    {
        for (int i = 0; i < numberOfHearts; i++)
        {
            AddCompleteHeart();
        }

        //Debug.Log($"List size of hearts: {heartImages.Count}");
    }

    public void AddCompleteHeart()
    {
        GameObject newImage = Instantiate(heartPrefab, heartParent);
        if (newImage.transform.GetChild(0).TryGetComponent(out Image image))
        {
            heartImages.Add(image);
        }
    }

    public void UpdateHearts(int amount)
    {
        numberOfHearts = amount;
        foreach (var img in heartImages)
        {
            img.enabled = false;
        }

        for (int i = 0; i < amount; i++)
        {
            heartImages[i].enabled = true;
        }
    }

    public static void UpdateNumberOfHearts(int amount)
    {
        OnNumberOfHeartsChanged?.Invoke(amount);
    }
}
