using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class PlayState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            if(owner.GameData.Player.HasTarget())
                owner.GameData.Player.Fsm.ChangeState<AttackState>();
            else
                owner.GameData.Player.Fsm.ChangeState<MoveState>();

            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                if (owner.GameData.Enemys[i].HasTarget())
                {
                    owner.GameData.Enemys[i].Fsm.ChangeState<AttackState>();
                }
                else
                    owner.GameData.Enemys[i].Fsm.ChangeState<MoveState>();
            }
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            bool isAllCharacterIdle = true;

            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                if (!owner.GameData.Enemys[i].Fsm.CheckCurrentState(typeof(IdleState)))
                {
                    isAllCharacterIdle = false;
                    break;
                }
            }
            if (isAllCharacterIdle && owner.GameData.Player.Fsm.CheckCurrentState(typeof(IdleState)))
                owner.Fsm.ChangeState<ReadyState>();
        }
    }
}