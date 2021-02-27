using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class LevelUpState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            if (owner.GameData.Exp >= 100)
            {
                owner.GameData.IsPlay = false;
                owner.LevelUp();
            }
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if(owner.GameData.IsPlay)
                owner.Fsm.ChangeState<ReadyState>();
        }
    }
}