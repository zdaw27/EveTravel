using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class ReadyState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
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