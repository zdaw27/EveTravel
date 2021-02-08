using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class PlayState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.Player.NextPos = owner.Player.transform.position + owner.Player.NextPos;
            owner.Player.Fsm.ChangeState(typeof(MoveState));
            owner.Enemy.Fsm.ChangeState(typeof(PathState));
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if (owner.Player.Fsm.CheckCurrentState(typeof(IdleState)) && owner.Enemy.Fsm.CheckCurrentState(typeof(IdleState)))
            {
                owner.Fsm.ChangeState(typeof(ReadyState));
            }
        }
    }
}