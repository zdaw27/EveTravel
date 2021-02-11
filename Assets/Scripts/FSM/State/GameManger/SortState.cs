using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class SortState : IState<GameManager>
    {

        bool isAllEnemyPathComplete = true;
        public void Enter(GameManager owner)
        {
            isAllEnemyPathComplete = false;
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
            isAllEnemyPathComplete = true;
            foreach (Enemy enemy in owner.GameData.Enemys)
            {
                if (!enemy.Fsm.GetState<PathState>().IsPathComplete)
                {
                    isAllEnemyPathComplete = false;
                    break;
                }
            }

            if (isAllEnemyPathComplete)
            {
                owner.GameData.Enemys.Sort(delegate (Enemy a, Enemy b)
                {
                    return a.Fsm.GetState<PathState>().Path.path.Count - b.Fsm.GetState<PathState>().Path.path.Count;
                });
                owner.Fsm.ChangeState(typeof(PlayState));
            }
        }

        
    }
}