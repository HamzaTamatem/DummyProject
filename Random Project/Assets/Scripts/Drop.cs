using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject prefabToDrop;

    public void DropPrefab()
    {
        GameObject newScrap = Instantiate(prefabToDrop, transform.position, Quaternion.identity);
        newScrap.GetComponent<Scrap>().xpLevel = Collectible.XpLevel.Two;
        newScrap.SetActive(true);
    }

    private void OnDestroy()
    {
        DropPrefab();
    }
}
