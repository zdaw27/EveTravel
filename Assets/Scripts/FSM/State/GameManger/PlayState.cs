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
            foreach(Enemy enemy in owner.GameData.Enemys)
            {
                enemy.Fsm.ChangeState(typeof(PathState));
            }
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            bool isAllEnemyIdle = true;
            foreach (Enemy enemy in owner.GameData.Enemys)
            {
                if(!enemy.Fsm.CheckCurrentState(typeof(IdleState)))
                {
                    isAllEnemyIdle = false;
                    break;
                }
            }
            if (owner.GameData.Player.Fsm.CheckCurrentState(typeof(IdleState)) && isAllEnemyIdle)
            {
                owner.Fsm.ChangeState(typeof(ReadyState));
            }
        }
    }
}