using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using EveTravel;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("game config")]
    [SerializeField] private float npcSpeed = 3f;
    [Header("runtime datas")]
    [SerializeField] private Item equiped;
    [SerializeField] private List<Item> inventory;
    [SerializeField] private Player player = null;
    [SerializeField] private List<Enemy> enemys;
    [SerializeField] private EveMap eveMap;

    public float NpcSpeed { get { return npcSpeed; } private set { } }
    public Item Equiped { get => equiped; set => equiped = value; }
    public List<Item> Inventory { get => inventory; set => inventory = value; }
    public Player Player { get => player; set => player = value; }
    public List<Enemy> Enemys { get => enemys; set => enemys = value; }
    public EveMap EveMap { get => eveMap; set => eveMap = value; }


}
