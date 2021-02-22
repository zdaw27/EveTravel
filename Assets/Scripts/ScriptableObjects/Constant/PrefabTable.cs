using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabTable", menuName = "ScriptableObjects/PrefabTable", order = 1)]
public class PrefabTable : ScriptableObject
{
    [SerializeField]
    private SerializableGameObjectHashSet playerPrefab;
    [SerializeField]
    private SerializableGameObjectHashSet enemyPrefab;
    [SerializeField]
    private SerializableGameObjectHashSet treasurePrefab;
    [SerializeField]
    private SerializableGameObjectHashSet exitPrefab;

    public SerializableGameObjectHashSet PlayerPrefab { get => playerPrefab; set => playerPrefab = value; }
    public SerializableGameObjectHashSet EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public SerializableGameObjectHashSet TreasurePrefab { get => treasurePrefab; set => treasurePrefab = value; }
    public SerializableGameObjectHashSet ExitPrefab { get => exitPrefab; set => exitPrefab = value; }
}
