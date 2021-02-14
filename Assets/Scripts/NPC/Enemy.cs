using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Enemy : Character
    {
        private void Awake()
        {
            fsm = new FSM<Character>(this, new IdleState());
            fsm.AddState(new MoveState());
            fsm.AddState(new AttackState());
        }

        public override void Attack()
        {
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.PlayerHit);
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.DamageEffect, stat.attack);
        }

        public void GetNextPos()
        {
            gameData.EveMap.GetNextPos(this);
        }

        public void GetAttackTarget()
        {
            if (Vector3.Distance(GameData.Player.transform.position, transform.position) <= 1f)
                attackTarget = GameData.Player;
            else
                attackTarget = null;
        }
    }
}