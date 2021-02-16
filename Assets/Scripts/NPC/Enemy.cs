using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Enemy : Character
    {
        public override void Attack()
        {
            base.Attack();
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.PlayerHit);
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