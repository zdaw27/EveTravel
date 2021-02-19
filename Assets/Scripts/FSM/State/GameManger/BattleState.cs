using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace EveTravel
{
    /// <summary>
    /// Enemy들의 타겟을 찾고, 타겟을 찾은 객체들은 AttackState 로 변경.
    /// Player의 타겟은 Input State 에서 결정, 공격은 여기서 실행.
    /// 전투 이후 Player 가 죽을경우, GameOverState로 변경.
    /// 전투 이후 살아남을 경우, PathFindState로 변경.
    /// </summary>
    public class BattleState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                owner.GameData.Enemys[i].GetAttackTarget();
            }

            bool isShake = false;
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                if (owner.GameData.Enemys[i].HasTarget())
                {
                    owner.GameData.Enemys[i].Attack();
                    
                    if (!isShake)
                    {
                        isShake = true;
                        Camera.main.DOShakePosition(0.5f, 1f, 20, 90, true);
                    }
                }
            }

            if(owner.GameData.Player.HasTarget())
            {
                owner.GameData.Player.Attack();
            }

            //전투 후, 죽은 Enemy들 Container에서 제외 및 DeathState로 처리.
            for(int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                if(owner.GameData.Enemys[i].Stat.hp <= 0)
                {
                    Enemy enemy = owner.GameData.Enemys[i];
                    owner.GameData.Enemys.Remove(enemy);
                    --i;
                    owner.AddGold(enemy.transform.position);
                    enemy.Death();
                    
                }
            }
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