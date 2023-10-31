using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap spikeTilemap;

    private static List<Vector3> spikeTilePositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(var pos in spikeTilemap.cellBounds.allPositionsWithin) {
            if (spikeTilemap.HasTile(pos))
            {
                spikeTilePositions.Add(new Vector3(pos.x+0.5f,pos.y+0.5f));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 GetClosestTile(Vector3 position)
    {
        foreach (var tile in spikeTilePositions)
        {
            if (Vector3.Distance(position, tile) < 1.2f)
            {
                Debug.Log($"Closest tile position: {tile}");
                return tile;
            }
        }

        Debug.Log("Vector3.zero???");
        return Vector3.zero;
    }
}
