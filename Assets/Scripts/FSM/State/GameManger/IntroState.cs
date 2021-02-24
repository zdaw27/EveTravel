using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class IntroState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.Intro();
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if(owner.GameData.IsPlay)
                owner.Fsm.ChangeState<ReadyState>();
        }
    }
}