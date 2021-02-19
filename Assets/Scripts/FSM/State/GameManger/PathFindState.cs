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
            owner.GameData.EveMap.ClearWakedIndices();
            this.owner = owner;
            //가까운 몬스터 먼저 패스파인딩 완료 하기위해 정렬.
            owner.GameData.Enemys.Sort((Enemy a, Enemy b) => 
            {
                if (Vector3.Distance(a.transform.position, owner.GameData.Player.NextPos) < Vector3.Distance(b.transform.position, owner.GameData.Player.NextPos))
                {
                    return -1;
                }
                else if (Vector3.Distance(a.transform.position, owner.GameData.Player.NextPos) > Vector3.Distance(b.transform.position, owner.GameData.Player.NextPos))
                    return 1;
                else

                    return 0;
            });
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                //owner.GameData.Enemys[i].Rotation(owner.GameData.Player.transform.position - owner.GameData.Enemys[i].transform.position);
                owner.GameData.Enemys[i].GetNextPos();
                if(owner.GameData.Enemys[i].transform.position != owner.GameData.Enemys[i].NextPos)
                    owner.GameData.Enemys[i].Move();
                else
                    owner.GameData.Enemys[i].IsIdle = true;
            }

            if(!owner.GameData.Player.HasTarget())
                owner.GameData.Player.Move();

            owner.Fsm.ChangeState<MoveNPCState>();
        }
    }
}