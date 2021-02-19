using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class ReadyState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.GameData.Player.ResetIdleState();
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                owner.GameData.Enemys[i].ResetIdleState();
            }

            if (owner.GameData.EveMap.PlayerInteraction(owner.GameData.Player.transform.position))
            {
                for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
                {
                    GameObject.Destroy(owner.GameData.Enemys[i].gameObject);
                }
                GameObject.Destroy(owner.GameData.EveMap.gameObject);
                owner.GameData.StageLevel ++;
                GameObject.Instantiate(owner.MapTable.Maps[owner.GameData.StageLevel]);
            }
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            //if(owner.GameData.IsPlay)
                owner.Fsm.ChangeState<InputState>();
        }
    }
}