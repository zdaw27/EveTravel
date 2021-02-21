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
    [SerializeField]
    private float npcSpeed = 3f;
    [Header("runtime datas")]
    [SerializeField]
    private Item equiped;
    [SerializeField]
    private List<Item> inventory;
    [SerializeField]
    private Player player = null;
    [SerializeField]
    private List<Enemy> enemys;
    [SerializeField]
    private EveMap eveMap;
    [SerializeField]
    private bool isPlay = false;
    [Header("player datas")]
    [SerializeField]
    private int gold = 0;
    [SerializeField]
    private int potion = 0;
    [SerializeField]
    private int stageLevel = 0;
    [SerializeField]
    private int exp = 0;
    [SerializeField]
    private int playerLevel = 1;

    public float NpcSpeed { get { return npcSpeed; } private set { } }
    public Item Equiped { get => equiped; set => equiped = value; }
    public List<Item> Inventory { get => inventory; set => inventory = value; }
    public Player Player { get => player; set => player = value; }
    public List<Enemy> Enemys { get => enemys; set => enemys = value; }
    public EveMap EveMap { get => eveMap; set => eveMap = value; }
    public bool IsPlay { get => isPlay; set => isPlay = value; }
    public int StageLevel { get => stageLevel; set => stageLevel = value; }
    public int Gold { get => gold; set => gold = value; }
    public int Potion { get => potion; set => potion = value; }
    public int Exp { get => exp; set => exp = value; }
    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }

    private void OnEnable()
    {
        PlayerLevel = 1;
        stageLevel = 0;
        gold = 0;
        potion = 0;
        exp = 0;
    }
}
