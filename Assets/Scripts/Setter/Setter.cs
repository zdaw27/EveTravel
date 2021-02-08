using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public abstract class Setter<T> : MonoBehaviour
    {
        [SerializeField] private GameData gameData;

        private void OnEnable()
        {
        }
    }
}