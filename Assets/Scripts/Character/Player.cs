using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Player : Character
    {
        [SerializeField]
        private GameEvent levelUpEvent;

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

        /// <summary>
        /// Player가 맵과 상호작용 로직.
        /// <para> example : Exit 입구 도착, 보물상자와 상호작용, 상점과 상호작용. </para>
        /// </summary>
        /// <param name="playerPos"></param>
        /// <returns> Exit에 도착했는가? </returns>
        public bool PlayerInteraction(EveMap eveMap)
        {
            int index = eveMap.GetIndex(transform.position);

            if (eveMap.TreasureIndex.Contains(index))
                eveMap.Treasures[index].Looting();

            if (eveMap.ExitIndex.Contains(index))
                return true;
            else
                return false;
        }

    }
}