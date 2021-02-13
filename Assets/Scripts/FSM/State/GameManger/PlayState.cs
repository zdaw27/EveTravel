using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class PlayState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.GameData.Player.Fsm.ChangeState(typeof(MoveState));
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
                owner.GameData.Enemys[i].Fsm.ChangeState(typeof(MoveState));
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            bool isAllNPCIdle = true;

            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                if (!owner.GameData.Enemys[i].Fsm.CheckCurrentState(typeof(IdleState)))
                {
                    isAllNPCIdle = false;
                    break;
                }
            }
            

            if (isAllNPCIdle && owner.GameData.Player.Fsm.CheckCurrentState(typeof(IdleState)))
                owner.Fsm.ChangeState(typeof(ReadyState));
        }
    }
}