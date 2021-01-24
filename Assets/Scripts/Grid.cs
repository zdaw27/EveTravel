using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;

    private int[,] gridArray;
    Rect rect;
    
    
    private void Test()
    {
    }
}

public class GridCell
{
    Vector2 position;
    CellType cellType;
}

public enum CellType
{
    Walkable,
    UnWalkable
}