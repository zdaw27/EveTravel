using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Player : Character
    {
        private void Awake()
        {
            fsm = new FSM<Character>(this, new IdleState());
            fsm.AddState(new MoveState());
            fsm.AddState(new AttackState());
        }

        public override void Attack()
        {
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.EnemyHit);
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.DamageEffect, stat.attack);
        }

    }
}