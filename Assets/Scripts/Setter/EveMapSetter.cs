using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [RequireComponent(typeof(EveMap))]
    public class EveMapSetter : MonoBehaviour
    {
        [SerializeField]
        private GameData gameData;

        protected void OnEnable()
        {
            gameData.EveMap = gameObject.GetComponent<EveMap>();
        }

        protected void OnDisable()
        {
            gameData.EveMap = null;
        }
    }
}