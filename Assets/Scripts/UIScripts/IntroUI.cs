using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class IntroUI : MonoBehaviour
    {
        [SerializeField]
        private GameEvent gameStartEvent;

        public void GameStart()
        {
            gameStartEvent.Raise();
            gameObject.SetActive(false);
        }
    }
}