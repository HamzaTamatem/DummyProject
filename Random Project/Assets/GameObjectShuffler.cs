using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectShuffler : MonoBehaviour
{
    [SerializeField] private GameObject[] tilemaps;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShuffleObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShuffleObjects()
    {
        while (true)
        {
            for (int i = 0; i < tilemaps.Length; i++)
            {
                yield return new WaitForSeconds(1f);
                for (int j = 0; j < tilemaps.Length; j++)
                {
                    if (i == j)
                    {
                        tilemaps[j].SetActive(true);
                    }
                    else
                    {
                        tilemaps[j].SetActive(false);
                    }
                }
            }
        }
    }
}
