using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EveMap : MonoBehaviour
{
    [SerializeField] private SerializableIntHashSet playerSpawnIndex;
    [SerializeField] private SerializableIntHashSet enemySpawnIndex;
    [SerializeField] private SerializableIntHashSet treasureIndex;
    [SerializeField] private SerializableIntHashSet exitIndex;
    [SerializeField] private SerializableIntHashSet storeIndex;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private List<TileCell> tileCell;

    public SerializableIntHashSet PlayerSpawnIndex { get => playerSpawnIndex; set => playerSpawnIndex = value; }
    public SerializableIntHashSet EnemySpawnIndex { get => enemySpawnIndex; set => enemySpawnIndex = value; }
    public SerializableIntHashSet TreasureIndex { get => treasureIndex; set => treasureIndex = value; }
    public SerializableIntHashSet ExitIndex { get => exitIndex; set => exitIndex = value; }
    public SerializableIntHashSet StoreIndex { get => storeIndex; set => storeIndex = value; }
    public List<TileCell> TileCell { get => tileCell; set => tileCell = value; }
    public int Height { get => height; set => height = value; }
    public int Width { get => width; set => width = value; }

    private void Start()
    {
        
    }
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

