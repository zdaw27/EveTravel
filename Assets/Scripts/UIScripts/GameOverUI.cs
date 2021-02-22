using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private GameEvent gameStartEvent;

        public void RestartGame()
        {
            gameStartEvent.Raise();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}