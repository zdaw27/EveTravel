using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
            bool isAllEnemyPathComplete = true;
            foreach (Enemy enemy in owner.GameData.Enemys)
            {
                if (!enemy.Fsm.GetState<PathState>().IsPathComplete)
                {
                    isAllEnemyPathComplete = false;
                    break;
                }
            }

            owner.GameData.Enemys.Sort(delegate (Enemy a, Enemy b)
            {
                return b.Fsm.GetState<PathState>().Path.path.Count - a.Fsm.GetState<PathState>().Path.path.Count;
            });

            foreach (Enemy enemy in owner.GameData.Enemys)
            {
                enemy.NextPos = (Vector3)enemy.Path.path[1].position;
                enemy.Blocker.BlockAt(enemy.NextPos);
            }

            if (owner.GameData.Player.Fsm.CheckCurrentState(typeof(IdleState)) && isAllEnemyIdle)
            {
                owner.Fsm.ChangeState(typeof(ReadyState));
            }
        }

        
    }
}