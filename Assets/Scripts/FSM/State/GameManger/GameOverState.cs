using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class GameOverState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.GameOver();
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
        }
    }
}