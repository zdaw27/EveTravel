using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [Header("game config")]
        [SerializeField] private float npcSpeed = 3f;
        [Header("runtime datas")]
        [SerializeField] private Item equiped;
        [SerializeField] private List<Item> inventory;

        public Player Player { get; set; }
        public List<Enemy> Enemys { get; set; } = new List<Enemy>();
        public Item Equiped { get; set; }
        public Item Inventory { get; set; }
        public float NpcSpeed { get { return npcSpeed; } private set { } }

    }
}