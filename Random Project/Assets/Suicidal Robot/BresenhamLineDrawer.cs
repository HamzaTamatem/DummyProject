using UnityEngine;
using System.Collections.Generic;

public class BresenhamLineDrawer : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public GameObject squarePrefab;
    public float gridSize = 1.0f;  // Grid size for placing squares

    private List<GameObject> squares = new List<GameObject>();
    private Vector3 lastPointAPosition;
    private Vector3 lastPointBPosition;

    // Grid scaling factor
    public float gridScalingFactor = 0.125f;

    void Update()
    {
        if (pointA.position != lastPointAPosition || pointB.position != lastPointBPosition)
        {
            DrawLine();
            lastPointAPosition = pointA.position;
            lastPointBPosition = pointB.position;
        }
    }

    void DrawLine()
    {
        ClearLine();

        Vector2 p0 = SnapToGrid(pointA.position);
        Vector2 p1 = SnapToGrid(pointB.position);

        int dx = Mathf.Abs(Mathf.RoundToInt((p1.x - p0.x) / (gridSize * gridScalingFactor)));
        int dy = -Mathf.Abs(Mathf.RoundToInt((p1.y - p0.y) / (gridSize * gridScalingFactor)));
        int sx = p0.x < p1.x ? 1 : -1;
        int sy = p0.y < p1.y ? 1 : -1;
        int err = dx + dy, e2;

        while (true)
        {
            GameObject square = Instantiate(squarePrefab, new Vector3(p0.x, p0.y, 0), Quaternion.identity);
            squares.Add(square);

            if (Mathf.RoundToInt(p0.x / (gridSize * gridScalingFactor)) == Mathf.RoundToInt(p1.x / (gridSize * gridScalingFactor)) &&
                Mathf.RoundToInt(p0.y / (gridSize * gridScalingFactor)) == Mathf.RoundToInt(p1.y / (gridSize * gridScalingFactor))) break;

            e2 = 2 * err;
            if (e2 >= dy) { err += dy; p0.x += sx * gridSize * gridScalingFactor; }
            if (e2 <= dx) { err += dx; p0.y += sy * gridSize * gridScalingFactor; }
        }
    }

    Vector2 SnapToGrid(Vector2 position)
    {
        return new Vector2(
            Mathf.Round(position.x / (gridSize * gridScalingFactor)) * gridSize * gridScalingFactor,
            Mathf.Round(position.y / (gridSize * gridScalingFactor)) * gridSize * gridScalingFactor
        );
    }

    void ClearLine()
    {
        foreach (GameObject square in squares)
        {
            Destroy(square);
        }
        squares.Clear();
    }
}
