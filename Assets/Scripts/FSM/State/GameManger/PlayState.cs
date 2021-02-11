using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class PlayState : IState<GameManager>
    {

        bool isAllEnemyPathComplete = true;
        public void Enter(GameManager owner)
        {
            isAllEnemyPathComplete = false;
            
            foreach(Enemy enemy in owner.GameData.Enemys)
            {
                enemy.Fsm.ChangeState(typeof(BlockPathState));
            }
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            isAllEnemyPathComplete = true;
            foreach (Enemy enemy in owner.GameData.Enemys)
            {
                if (!enemy.Fsm.GetState<BlockPathState>().IsPathComplete)
                {
                    isAllEnemyPathComplete = false;
                    break;
                }
            }
            if (isAllEnemyPathComplete)
            {
                owner.GameData.Player.Fsm.ChangeState(typeof(MoveState));
                foreach (Enemy enemy in owner.GameData.Enemys)
                {
                    enemy.Fsm.ChangeState(typeof(MoveState));
                }
            }

            if (owner.GameData.Player.Fsm.CheckCurrentState(typeof(IdleState)))
            {
                owner.Fsm.ChangeState(typeof(ReadyState));
            }
        }

        
    }
}