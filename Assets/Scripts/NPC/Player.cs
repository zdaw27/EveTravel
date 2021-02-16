using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Player : Character
    {
        public override void Attack()
        {
            base.Attack();
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.EnemyHit);
        }
        
        public void SetAttackTarget(Character attackTarget)
        {
            this.attackTarget = attackTarget;
        }

        public void SetNextPos(Vector3 nexPos)
        { 
            NextPos = nexPos;
        }
    }
}