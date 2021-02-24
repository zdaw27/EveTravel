using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class MapChangeState : IState<GameManager>
    {
        bool endOneFrame = false;

        public void Enter(GameManager owner)
        {
            endOneFrame = false;
            owner.GameData.StageLevel++;
            owner.ChangeMap();
            owner.GameData.IsPlay = true;
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if(endOneFrame)
                owner.Fsm.ChangeState<ReadyState>();
            endOneFrame = true;
        }
    }
}