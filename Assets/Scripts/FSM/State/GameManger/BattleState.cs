using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace EveTravel
{
    public class BattleState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.Battle();
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if (owner.GameData.Player.Stat.hp <= 0)
                owner.Fsm.ChangeState<GameOverState>();
            else
                owner.Fsm.ChangeState<PathFindState>();
        }
    }
}
