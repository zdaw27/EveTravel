using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [RequireComponent(typeof(Player))]
    public class PlayerSetter : MonoBehaviour
    {
        [SerializeField] private GameData gameData;

        protected void OnEnable()
        {
            gameData.Player = gameObject.GetComponent<Player>();
        }

        protected void OnDisable()
        {
            gameData.Player = null;
        }
    }
}