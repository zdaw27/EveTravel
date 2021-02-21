using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Player : Character
    {
        [SerializeField] private GameEvent levelUpEvent;

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

        public void LevelUP()
        {
            gameData.Exp = 0;
            gameData.PlayerLevel++;
            levelUpEvent.Raise();
        }

        public void EarnExp()
        {
            gameData.Exp += 10;
            if(gameData.Exp >= 100)
            {
                LevelUP();
            }
        }

    }
}