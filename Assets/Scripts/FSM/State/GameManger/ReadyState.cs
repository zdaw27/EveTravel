using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class ReadyState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            if (owner.CheckNextLevel())
            {
                owner.GameData.IsPlay = false;
                owner.Fsm.ChangeState<MapChangeState>();
            }
            else
                owner.CheckPlayerInteraction();
        }

        public void Exit(GameManager owner)
        {

        }

        public void Update(GameManager owner)
        {
            //if(owner.GameData.IsPlay)
                owner.Fsm.ChangeState<InputState>();
        }
    }
}