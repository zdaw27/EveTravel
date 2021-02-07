﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EveMap : MonoBehaviour
{
    [SerializeField] private List<int> spawnIndex;
    [SerializeField] private List<int> treasureIndex;
    [SerializeField] private List<int> tileIndex;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private List<TileCell> tileCell;

    public List<TileCell> TileCell { get => tileCell; set => tileCell = value; }
    public int Height { get => height; set => height = value; }
    public int Width { get => width; set => width = value; }
}

[Serializable]
public class TileCell
{
     [SerializeField] private TileType type;

    public TileType Type { get => type; set => type = value; }

    static public TileCell Create(TileType type)
    {
        TileCell newTile = new TileCell();
        newTile.Type = type;
        return newTile;
    }
}

[Serializable]
public enum TileType
{
    walkable,
    noneWlakable
}
