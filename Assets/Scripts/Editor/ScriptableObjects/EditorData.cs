using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EditorData", menuName = "ScriptableObjects/Editor/EditorData", order = 1)]
public class EditorData : ScriptableObject
{
    [SerializeField] private EveMap eveMap;
    [SerializeField] private GameObject tile1;
    [SerializeField] private GameObject tile2;
    [SerializeField] private GameObject wallTile;
    [SerializeField] private int width;
    [SerializeField] private int height;

    public EveMap EveMap { get => eveMap; set => eveMap = value; }
    public GameObject Tile1 { get => tile1; set => tile1 = value; }
    public GameObject Tile2 { get => tile2; set => tile2 = value; }
    public GameObject WallTile { get => wallTile; set => wallTile = value; }
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    
}
