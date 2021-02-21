using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySetter : MonoBehaviour
    {
        [SerializeField]
        private GameData gameData;

        protected void OnEnable()
        {
            gameData.Enemys.Add(GetComponent<Enemy>());
        }

        protected void OnDisable()
        {
            gameData.Enemys.Remove(GetComponent<Enemy>());
        }
    }
}