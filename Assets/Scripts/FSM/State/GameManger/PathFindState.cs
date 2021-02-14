using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class PathFindState : IState<GameManager>
    {
        private GameManager owner;
        private readonly List<Vector3> checkList = new List<Vector3>() { Vector3.left, Vector3.up, Vector3.right, Vector3.down };
        //패스 계산중에 완료된 목록 저장용도.
        private HashSet<int> walkedIndices = new HashSet<int>();
        private List<Vector3> tileCandidate = new List<Vector3>();

        public void Enter(GameManager owner)
        {
            walkedIndices.Clear();
            walkedIndices.Add(owner.GameData.EveMap.GetIndex(owner.GameData.Player.NextPos));
            this.owner = owner;
            //가까운 몬스터 먼저 패스파인딩 완료 하기위해 정렬.
            owner.GameData.Enemys.Sort((Enemy a, Enemy b) => { return (int)(Vector3.Distance(a.transform.position, owner.GameData.Player.NextPos) * 10f - Vector3.Distance(b.transform.position, owner.GameData.Player.NextPos) * 10f); });
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            for (int enemyIdx = 0; enemyIdx < owner.GameData.Enemys.Count; ++enemyIdx)
            {
                owner.GameData.Enemys[enemyIdx].NextPos = owner.GameData.Enemys[enemyIdx].transform.position;
                tileCandidate.Clear();
                //owner.EffectListener.RaiseEffect(owner.GameData.Enemys[enemyIdx].transform.position, EffectManager.EffectType.DamageEffect, enemyIdx);

                for (int i = 0; i < checkList.Count; ++i)
                {
                    if (owner.GameData.EveMap.CheckWalkablePosition(owner.GameData.Enemys[enemyIdx].transform.position + checkList[i])
                        && EveUtil.IsIn180Angle(owner.GameData.Enemys[enemyIdx].transform.position, owner.GameData.Player.NextPos, owner.GameData.Enemys[enemyIdx].transform.position + checkList[i]))
                    {
                         tileCandidate.Add(owner.GameData.Enemys[enemyIdx].transform.position + checkList[i]);
                    }
                }

                for (int i = 0; i < tileCandidate.Count; ++i)
                {
                    if (!walkedIndices.Contains(owner.GameData.EveMap.GetIndex(tileCandidate[i])))
                    {
                        owner.GameData.Enemys[enemyIdx].NextPos = tileCandidate[i];
                        walkedIndices.Add(owner.GameData.EveMap.GetIndex(owner.GameData.Enemys[enemyIdx].NextPos));
                        //owner.EffectListener.RaiseEffect(owner.GameData.Enemys[enemyIdx].transform.position, EffectManager.EffectType.DamageEffect, tileCandidate.Count);
                        break;
                    }
                }

                owner.GameData.Enemys[enemyIdx].SetTarget(null);
                if (Vector3.Distance(owner.GameData.Enemys[enemyIdx].transform.position, owner.GameData.Player.transform.position) <= 1f)
                {
                    owner.GameData.Enemys[enemyIdx].NextPos = owner.GameData.Enemys[enemyIdx].transform.position;
                    owner.GameData.Enemys[enemyIdx].SetTarget(owner.GameData.Player);
                }

                if (owner.GameData.Enemys[enemyIdx].NextPos == owner.GameData.Enemys[enemyIdx].transform.position)
                {
                    walkedIndices.Add(owner.GameData.EveMap.GetIndex(owner.GameData.Enemys[enemyIdx].transform.position));
                }
            }

            owner.GameData.Player.Fsm.ChangeState<MoveState>();
            for (int enemyIdx = 0; enemyIdx < owner.GameData.Enemys.Count; ++enemyIdx)
            {
                owner.GameData.Enemys[enemyIdx].Fsm.ChangeState<MoveState>();
            }

            owner.Fsm.ChangeState<PlayState>();
        }

    }
}