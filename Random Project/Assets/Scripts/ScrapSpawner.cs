using System;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{

    public static event Action<Collectible.XpLevel, Vector3> OnEnemyDeath;

    [SerializeField] private GameObject scrapPrefab;

    private void OnEnable()
    {
        OnEnemyDeath += SpawnScrap;
    }

    private void OnDisable()
    {
        OnEnemyDeath -= SpawnScrap;
    }

    public void SpawnScrap(Collectible.XpLevel xpLevel, Vector3 scrapPosition)
    {
        GameObject newScrap = Instantiate(scrapPrefab, scrapPosition, Quaternion.identity);
        newScrap.GetComponent<Scrap>().SetXpLevel(xpLevel);
        newScrap.SetActive(true);
    }

    public static void NewEnemyDied(Collectible.XpLevel xpLevel, Vector3 position)
    {
        OnEnemyDeath?.Invoke(xpLevel, position);
    }
}
