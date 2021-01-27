using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public static GameData I { get; set; }

        public Player player { get; set; }
        public List<Enemy> Enemys { get; set; }

        private void OnEnable()
        {
            I = this;
        }

        private void OnDisable()
        {
            
        }
    }
}