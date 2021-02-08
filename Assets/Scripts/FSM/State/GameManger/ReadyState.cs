using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class ReadyState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            owner.Fsm.ChangeState(typeof(InputState));
        }
    }
}