using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EditorData", menuName = "ScriptableObjects/Editor/EditorData", order = 1)]
public class EditorData : ScriptableObject
{
    private EveMap eveMap;
    private GameObject tile1;
    private GameObject tile2;
    private int width;
    private int height;

    public EveMap EveMap { get => eveMap; set => eveMap = value; }
    public GameObject Tile1 { get => tile1; set => tile1 = value; }
    public GameObject Tile2 { get => tile2; set => tile2 = value; }
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
}
