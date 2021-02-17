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
            bool isAllCharacterIdle = true;

            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                if (!owner.GameData.Enemys[i].IsIdle)
                {
                    isAllCharacterIdle = false;
                    break;
                }
            }
            if (isAllCharacterIdle && owner.GameData.Player.IsIdle)
                owner.Fsm.ChangeState<ReadyState>();
        }
    }
}