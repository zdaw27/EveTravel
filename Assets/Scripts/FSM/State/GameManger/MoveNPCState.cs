using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class MoveNPCState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if(owner.CheckAllCharacterMoveComplete())
                owner.Fsm.ChangeState<ReadyState>();
        }
    }
}