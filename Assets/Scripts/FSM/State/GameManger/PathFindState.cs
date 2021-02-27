using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    /// <summary>
    /// 어택하지 않는 몬스터 길찾기 State.
    /// </summary>
    public class PathFindState : IState<GameManager>
    {
        private GameManager owner;
        
        public void Enter(GameManager owner)
        {
            owner.PathFinding();
            owner.StartCharactersMove();
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if (owner.CheckAllCharacterMoveComplete() && owner.GameData.IsPlay)
                owner.Fsm.ChangeState<LevelUpState>();
        }
    }
}